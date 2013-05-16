using System;
using System.IO.Ports;
using System.Text;
using Nucleo.Communications;
using Nucleo.Events;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.GoodGuide.Types.Events.ControlEvents;
using Nucleo.Gps.Nmea;
using Buffer=Nucleo.Communications.Buffer;
using SerialPort=System.IO.Ports.SerialPort;

namespace Nucleo.GoodGuide.SerialGps
{
  public class SerialGpsAdapter
  {
    public const float ValidHeadingMinimumSpeed = 2;
    public const Int32 DefaultBaudRate = 9600;

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

    public SerialGpsAdapter(INamedParameterRepository parameterRepository)
    {
      this.parameterRepository = parameterRepository;

      framedMessage = new BytePatternFramedMessage(receiveBuffer);
      framedMessage.FrameStart = new byte[1] { (byte)'$' };
      framedMessage.FrameEnd = new byte[1] { 10 };
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
      if (isRunning == true)
        return;

      try
      {
        serialPort = new SerialPort(portName,baudRate);
        serialPort.DataReceived += SerialPort_DataReceived;
        serialPort.Open();
        isRunning = true;
      }
      catch (Exception exc)
      {
        LoggingHelper.Write(this,string.Format("Error starting GpsAdapter: {0}",ExceptionManager.Message(exc)));
      }
    }
    public void Stop()
    {
      if (isRunning == false)
        return;

      serialPort.DataReceived -= SerialPort_DataReceived;
      serialPort.DiscardInBuffer();
      serialPort.DiscardOutBuffer();
      serialPort.Close();
      serialPort.Dispose();
      isRunning = false;
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
          portName = "COM1";
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
    
    private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
    {
      if (isRunning == false)
        return;

      #region Read data from serial port
      byte[] buffer = new byte[serialPort.BytesToRead];
      int byteCount = serialPort.Read(buffer,0,serialPort.BytesToRead);
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
          eventSequence++;
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

    private readonly INamedParameterRepository parameterRepository = null;
    private SerialPort serialPort = null;
    private Int32 eventSequence = 0;
    private string rmcData = string.Empty;
    private string vtgData = string.Empty;
    private string ggaData = string.Empty;
    private Boolean isRunning = false;
    private string portName = "COM2";
    private Int32 baudRate = 9600;
    private Nucleo.Communications.Buffer receiveBuffer = new Buffer(10000);
    private Nucleo.Communications.BytePatternFramedMessage framedMessage;

    public LoggingHelper LoggingHelper = new LoggingHelper();

  }
}