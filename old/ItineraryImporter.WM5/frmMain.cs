using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.ItineraryImporter
{
  public delegate void UpdateTextDelegate();

  public partial class frmMain : Form
  {
    public frmMain(ImporterContext context,string itineraryRefNo)
    {
      Guard.ArgumentNotNull(context, "context");
      Guard.ArgumentNotNullOrEmptyString(itineraryRefNo, "itineraryRefNo");

      InitializeComponent();

      this.context = context;
      this.itineraryRefNo = itineraryRefNo;
    }

    private void LogProgress(string text)
    {
      context.Logger.Write(this, text);
      listBox1.Items.Add(text);
      Application.DoEvents();
    }
    private void LogProgress(string text, Exception exc)
    {
      string message = ExceptionManager.MessageWithStackTrace(text, exc);
      LogProgress(message);
    }
    private void Import()
    {
      #region Check if file exists
      string filename = string.Format("{0}{1}.ggi", Path.ExecutablePath,itineraryRefNo);
      LogProgress(string.Format("Checking if file {0} exists", filename));

      if (File.Exists(filename) == false)
      {
        string message = string.Format("File {0} not found", filename);
        LogProgress(message);
        MessageBox.Show(message);

        errorOccurred = true;
        return;
      }
      #endregion

      #region Import itinerary
      context.ItinerarySyncBll.SyncStep += ItinerarySyncBll_SyncStep;
      try
      {
        context.ItinerarySyncBll.ImportFile(filename, 0, false);
      }
      catch (Exception exc)
      {
        LogProgress("Error importing itinerary", exc);
        errorOccurred = true;
      }
      context.ItinerarySyncBll.SyncStep -= ItinerarySyncBll_SyncStep;
      #endregion

      #region Delete file
      LogProgress(string.Format("Deleting itinerary file {0}",filename));
      try
      {
        File.Delete(filename);
      }
      catch (Exception exc)
      {
        LogProgress(string.Format("Error deleting itinerary file {0}", filename));
        errorOccurred = true;
      }
      #endregion

      #region Get itinerary
      LogProgress("Getting itinerary info");
      ItineraryDataset.ItineraryRow row = null;
      try
      {
        row = context.ItineraryBll.GetFirst();
      }
      catch (Exception exc)
      {
        LogProgress("Error getting itinerary",exc);
        errorOccurred = true;

        return;
      }

      if (row == null)
      {
        LogProgress("Itinerary not found");
        errorOccurred = true;

        return;
      }
      #endregion

      if (errorOccurred == false)
      {
        StringBuilder successMsg = new StringBuilder();

        successMsg.Append("Itinerary import successful\r\n");
        successMsg.Append(string.Format("Booking Ref: {0}\r\n", itineraryRefNo));
        successMsg.Append(string.Format("Booking For: {0} {1}\r\n", row.sFirstName, row.sLastName));
        successMsg.Append(string.Format("Arrival: {0}\r\n", row.sArrivalDat));
        successMsg.Append(string.Format("Departure: {0}\r\n", row.sDepartureDat));

        MessageBox.Show(successMsg.ToString());
      }
    }

    private void frmMain_Load(object sender, EventArgs e)
    {
      timStart.Enabled = true;
    }
    private void timStart_Tick(object sender, EventArgs e)
    {
      timStart.Enabled = false;

      Import();
    }
    private void ItinerarySyncBll_SyncStep(object sender, Nucleo.Xml.XmlDataSetEventArgs e)
    {
      LogProgress(string.Format("Syncing {0}",e.Text));
    }

    private readonly ImporterContext context;
    private readonly string itineraryRefNo;
    private Boolean errorOccurred = false;

  }
}