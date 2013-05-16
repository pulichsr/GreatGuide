using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmLoadItinerary : Form
  {
    public static void ShowForm()
    {
      frmLoadItinerary frm = new frmLoadItinerary();
      frm.ShowDialog();
      frm.Dispose();
    }

    internal frmLoadItinerary()
    {
      InitializeComponent();
    }

    private void SyncStep(object sender,XmlDataSetEventArgs e)
    {
      lblFileStep.Text = e.Text;
      Application.DoEvents();
    }

    private void frmLoadData_Load(object sender, EventArgs e)
    {
      edtCurrentFile.Text = CarApplication.Instance.LoadedItineraryFile;

      syncBll.SyncStep += SyncStep;
    }
    private void frmLoadData_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      syncBll.SyncStep -= SyncStep;
    }
    private void btnLoadFile_Click(object sender, EventArgs e)
    {
      CarApplication.Instance.WriteLog(this, ">> btnLoadFile_Click");
      try
      {
        CarApplication.Instance.ItinerarySyncBll.SyncStep += SyncStep;
        CarApplication.Instance.LoadItineraryFromCard();
        CarApplication.Instance.ItinerarySyncBll.SyncStep -= SyncStep;
      }
      catch (Exception exc)
      {
        CarApplication.Instance.WriteLog(this,"Error loading itinerary from card",exc);
        frmFactoryError.ShowException(exc);
        return;
      }

      edtCurrentFile.Text = CarApplication.Instance.LoadedItineraryFile;
    }

    private readonly SyncBll syncBll = CarApplication.Instance.ItinerarySyncBll;
  }
}