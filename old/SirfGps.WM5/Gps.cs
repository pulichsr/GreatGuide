using System;
using System.Text;
using System.Threading;
using Nucleo.Communications;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.Gps.Nmea;
using Buffer=Nucleo.Communications.Buffer;

namespace Nucleo.GoodGuide.SirfGps
{
  public class Gps
  {
    public const float ValidHeadingMinimumSpeed = 2;
    public const string DefaultComPort = "COM1";
    public const Int32 DefaultBaudRate = 38400;
    public const Int32 DefaultReceiveBufferLength = 10000;
    public const Int32 ReadInterval = 100;
    public const Int32 StopWaitTime = 10000;
    public const bool ResetEventExitContext = false;

    public Gps(INamedParameterRepository parameterRepository)
    {
      this.parameterRepository = parameterRepository;

      framedMessage = new BytePatternFramedMessage(receiveBuffer);
      framedMessage.FrameStart = new byte[1] { (byte)'$' };
      framedMessage.FrameEnd = new byte[1] { 10 };
    }

    #region Event broker
    [EventSubscriber(EventTopics.GpsAdapter.RunState)]
    public void RunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent)
      {
        RunStateEvent Event = (RunStateEvent)e.EventData;
        if (Event.IsRunning == true)
          Start();
        else 
          Stop();
      }
    }

    [EventSubscriber(EventTopics.GpsAdapter.RequestRunState)]
    public void RequestRunStateHandler(object sender, GoodGuideEventArgs e)
    {
      if (e.EventData is RunStateEvent)
      {
        RunStateEvent Event = (RunStateEvent)e.EventData;
        Event.IsRunning = isRunning;
      }
    }
    #endregion

    [EventPublisher(EventTopics.GpsAdapter.GpsRawData)]
    public event EventHandler<GoodGuideEventArgs> NewGpsRawData;

    public Boolean IsRunning
    {
      get { return isRunning; }
    }

    public void Initialise()
    {
      LoadConfiguration();
    }
    public void Finalise()
    {
    }

    public void Start()
    {
      if (isRunning)
        return;

      try
      {
        if (string.IsNullOrEmpty(portName) == true)
        {
          LoggingHelper.Write(this, "COM port is not defined. Not starting");
          return;
        }

        string sirfPortName = portName + ":";
        LoggingHelper.Write(this,string.Format("Creating Sirf port on {0}",sirfPortName));
        serialPort = new SirfSerialPort(sirfPortName, baudRate);
        serialPort.Open();
        ClearBuffer();

        LoggingHelper.Write(this, "Starting...");
        stopSignal.Reset();
        ThreadStart start = ReadThread;
        Thread readThread = new Thread(start);
        readThread.Name = string.Format("{0}.ReadThread", this.GetType().FullName);
        readThread.Start();
        LoggingHelper.Write(this, "Started");
      }
      catch (Exception exc)
      {
        LoggingHelper.Write(this,string.Format("Error starting GpsAdapter: {0}",ExceptionManager.Message(exc)));
      }
    }
    public void Stop()
    {
      if (IsRunning == false)
        return;

      try
      {
        LoggingHelper.Write(this, "Stopping...");

        stoppedSignal.Reset();
        stopSignal.Set();
        stoppedSignal.WaitOne(StopWaitTime, ResetEventExitContext);

        serialPort.Close();

        isRunning = false;
        LoggingHelper.Write(this, "Stopped");
      }
      catch (Exception exc)
      {
        LoggingHelper.Write(this, "Error stopping", exc);
      }
    }

    #region Event dispatchers
    private void OnNewGpsRawData(GpsRawEvent gpsRawEvent)
    {
      if (NewGpsRawData == null)
        return;

      try
      {
        GoodGuideEventArgs Event = new GoodGuideEventArgs(GetType().Name,gpsRawEvent);
        NewGpsRawData(this, Event);
      }
      catch
      {        
      }
    }
    #endregion

    private void LoadConfiguration()
       
    {
      LoggingHelper.Write(this, "Loading configuration");
      try
      {
        portName = parameterRepository.GetString(ConfigurationParameters.Gps.PortName);
        if (string.IsNullOrEmpty(portName) == true)
        {
          portName = DefaultComPort;
          parameterRepository.SetString(ConfigurationParameters.Gps.PortName,portName);
        }

        baudRate = parameterRepository.GetInt32(ConfigurationParameters.Gps.BaudRate) ?? DefaultBaudRate;
        
        LoggingHelper.Write(this, string.Format("COM port {0}", portName));
        LoggingHelper.Write(this, string.Format("Baud rate {0}", baudRate));
      }
      catch (Exception exc)
      {
        LoggingHelper.Write(this, "Error loading configuration", exc);

      }
    }

    private void ReadThread()
    {
      isRunning = true;
      while (true)
      {
        Boolean stopSignalled = stopSignal.WaitOne(0, ResetEventExitContext);
        if (stopSignalled == true)
          break;

        try
        {
          ReadData();
        }
        catch (Exception exc)
        {
          LoggingHelper.Write(this, "Error in ReadThread", exc);
        }
      }

      stoppedSignal.Set();
      isRunning = false;
    }
    private void ReadData()
    {
      if (isRunning == false)
        return;

      #region Read data from serial port
      byte[] buffer = new byte[DefaultReceiveBufferLength];
      uint receiveBufferLength = DefaultReceiveBufferLength;
      int byteCount = serialPort.Read(buffer, ref receiveBufferLength);
      if (byteCount == 0)
        Thread.Sleep(250);
      #endregion

      #region Append new data to receive buffer
      byte[] bytesRead = new byte[byteCount];
      System.Buffer.BlockCopy(buffer,0,bytesRead,0,(int)byteCount);
      receiveBuffer.Append(bytesRead);
      #endregion

      while (true)
      {
        try
        {
          #region Extract message
          byte[] messageData = framedMessage.ExtractNext();
          if (messageData == null)
            break;
          #endregion

          string rawData = Encoding.ASCII.GetString(messageData,0,messageData.Length);

          rawData = rawData.TrimEnd((char)10);
          rawData = rawData.TrimEnd((char)13);
          LoggingHelper.Write(this, rawData);

          if (rawData.StartsWith("$" + GpRmc.Prefix) == true)
            rmcData = rawData;
          if (rawData.StartsWith("$" + GpGga.Prefix) == true)
            ggaData = rawData;
          if (rawData.StartsWith("$" + GpVtg.Prefix) == true)
            vtgData = rawData;

          Boolean hasGga = string.IsNullOrEmpty(ggaData) == false;
          Boolean hasRmc = string.IsNullOrEmpty(rmcData) == false;
          Boolean hasVtg = string.IsNullOrEmpty(vtgData) == false;

          if (!hasGga || !hasRmc || !hasVtg)
            continue;

          #region Send new raw data message
          LoggingHelper.Write(this, "Sending GPS raw event");
          GpsRawEvent gpsRawEvent = new GpsRawEvent(GpsPositionSources.Gps,eventSequence,rmcData, vtgData, ggaData);
          OnNewGpsRawData(gpsRawEvent);
          #endregion

          ggaData = string.Empty;
          rmcData = string.Empty;
          vtgData = string.Empty;
        }
        catch (Exception exc)
        {
          LoggingHelper.Write(this,"Error getting next message frame",exc);
          return;
        }
      }
    }
    private void ClearBuffer()
    {
      while (true)
      {
        byte[] buffer = new byte[DefaultReceiveBufferLength];
        uint receiveBufferLength = DefaultReceiveBufferLength;
        int byteCount = serialPort.Read(buffer,ref receiveBufferLength);
        if (byteCount == 0)
          break;
      }
    }

    private readonly INamedParameterRepository parameterRepository = null;
    private SirfSerialPort serialPort = null;
    private string rmcData = string.Empty;
    private string vtgData = string.Empty;
    private string ggaData = string.Empty;
    private Boolean isRunning = false;
    private Int32 eventSequence = 0;
    private string portName = DefaultComPort;
    private Int32 baudRate = DefaultBaudRate;
    private readonly Nucleo.Communications.Buffer receiveBuffer = new Buffer(DefaultReceiveBufferLength);
    private readonly Nucleo.Communications.BytePatternFramedMessage framedMessage;

    public LoggingHelper LoggingHelper = new LoggingHelper();

    private readonly ManualResetEvent stopSignal = new ManualResetEvent(false);
    private readonly ManualResetEvent stoppedSignal = new ManualResetEvent(false);
  }
}