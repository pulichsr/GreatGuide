using System;
using System.Data;
using System.IO;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.Bll
{
  public class SyncBll
  {
    public const string SanityCheckErrorFilename = "SanityCheck.err";

    public SyncBll(ILogger logger)
    {
      Guard.ArgumentNotNull(logger, "logger");

      this.logger = logger;
    }

    public event EventHandler<XmlDataSetEventArgs> SyncStep;

    public SyncTargets SyncTargets
    {
      get { return syncTargets; }
    }
    public SyncSources SyncSources
    {
      get { return syncSources; }
    }

    public void DeleteAll()
    {
      #region Insert
      for (Int32 BllNo = 0; BllNo < syncTargets.Count; BllNo++)
      {
        OnSyncStep(string.Format("Deleting {0}...", syncTargets[BllNo].DatasetName));
        syncTargets[BllNo].DeleteAll();
      }
      #endregion
    }
    public string Import(Int32 offset, Boolean merge)
    {
      string path = string.Format("{0}Waypoints{1}", Nucleo.Path.ExecutablePath, System.IO.Path.DirectorySeparatorChar);
      if (Directory.Exists(path) == false)
        Directory.CreateDirectory(path);

      return ImportFromPath(path,offset,merge);
    }
    public string ImportFromPath(string path, Int32 offset, Boolean merge)
    {
      string[] files = Directory.GetFiles(path, "*.xml");
      if (files.Length == 0)
        return null;

      ImportFile(files[0],offset,merge);

      return files[0];
    }
    public void ImportFile(string filename, Int32 offset, Boolean merge)
    {
      logger.Write(this, ">> ImportFile");
      if (System.IO.File.Exists(filename) == false)
        throw new FileNotFoundException(string.Format("File {0} does not exist", filename));

      logger.Write(this, "   Getting data ...");
      OnSyncStep("Getting data ...");

      getDataError = false;

      GetDataResponse response = LoadFromFile(filename);

      if (getDataError == true)
      {
        logger.Write(this, "   Get data error");
        return;
      }

      #region Insert
      for (Int32 BllNo = 0; BllNo < syncTargets.Count; BllNo++)
      {
        logger.Write(this, string.Format("Importing {0}...", syncTargets[BllNo].DatasetName));
        OnSyncStep(string.Format("Importing {0}...", syncTargets[BllNo].DatasetName));
        syncTargets[BllNo].Insert(response,offset,merge);
      }
      #endregion

      logger.Write(this, "   Complete");
      OnSyncStep("Complete");
    }

    public void ExportFile(string filename)
    {
      GetDataResponse ExportData = new GetDataResponse();
      ExportData.RootElementName = "Data";
      for (Int32 BllNo = 0; BllNo < syncSources.Count; BllNo++)
      {
        OnSyncStep(string.Format("Exporting {0}...", syncTargets[BllNo].DatasetName));

        DataTable table = syncSources[BllNo].Get();
        table.DataSet.Tables.Remove(table);

        ExportData.Tables.Add(table);
      }

      ExportData.OnStartDataTable += StartDataTableHandler;
      ExportData.OnError += ErrorHandler;
      StringWriter writer;

      try
      {
        writer = ExportData.Encode(new EncoderFactory());
      }
      finally
      {
        ExportData.OnStartDataTable -= StartDataTableHandler;
        ExportData.OnError -= ErrorHandler;
      }

      TextWriter fileWriter = new StreamWriter(filename);
      try
      {
        fileWriter.Write(writer.ToString());
      }
      finally
      {
        fileWriter.Flush();
        fileWriter.Close();
      }
    }
    public GetDataResponse LoadFromFile(string filename)
    {
      TextReader Reader = new StreamReader(filename);
      GetDataResponse response = new GetDataResponse();
      response.RootElementName = "Data";
      DecoderFactory DecoderFactory = new DecoderFactory();

      response.OnStartDataTable += StartDataTableHandler;
      response.OnError += ErrorHandler;

      response.Decode(DecoderFactory, Reader);

      response.OnStartDataTable -= StartDataTableHandler;
      response.OnError -= ErrorHandler;

      Reader.Close();

      return response;
    }

    private void StartDataTableHandler(object sender, XmlDataSetEventArgs e)
    {
      logger.Write(this,string.Format("Syncing table: {0}",e.Text));
      OnSyncStep(e.Text);
    }
    private void ErrorHandler(object sender, XmlDataSetErrorEventArgs e)
    {
      getDataError = true;
      logger.Write(this, e.Text + " " + e.Error);
      OnSyncStep(e.Text + " " + e.Error);
    }

    private void OnSyncStep(string text)
    {
      if (SyncStep == null)
      return;
      
      XmlDataSetEventArgs Args = new XmlDataSetEventArgs(text);
      SyncStep(this,Args);
    }

    private readonly ILogger logger;
    private Boolean getDataError;
    private readonly SyncTargets syncTargets = new SyncTargets();
    private readonly SyncSources syncSources = new SyncSources();
  }

}
