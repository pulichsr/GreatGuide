using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmLoadDestinations : Form
  {
    public static void ShowForm()
    {
      frmLoadDestinations frm = new frmLoadDestinations();
      frm.ShowDialog();
      frm.Dispose();
    }

    internal frmLoadDestinations()
    {
      InitializeComponent();
    }

    private void SyncStep(object sender,XmlDataSetEventArgs e)
    {
      lblStep.Text = e.Text;
      System.Windows.Forms.Application.DoEvents();
    }

    private void frmLoadData_Load(object sender, EventArgs e)
    {
      edtCurrentFile.Text = CarApplication.Instance.LoadedContentFile;

      syncBll.SyncStep += SyncStep;
    }
    private void frmLoadData_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
      syncBll.SyncStep -= SyncStep;
    }
    private void btnOpenFile_Click(object sender, EventArgs e)
    {
      if (odFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
        return;

      edtFilename.Text = odFile.FileName.Trim();
    }
    private void btnLoadFile_Click(object sender, EventArgs e)
    {
      if (edtFilename.Text.Trim() == string.Empty)
      {
        MessageBox.Show("Invalid filename");
        return;
      }

      try
      {
        syncBll.ImportFile(edtFilename.Text,0,false);
      }
      catch (Exception exc)
      {
        frmFactoryError.ShowException(exc);
        return;
      }

      CarApplication.Instance.LoadedDestinationFile = System.IO.Path.GetFileNameWithoutExtension(edtFilename.Text);
      edtCurrentFile.Text = CarApplication.Instance.LoadedDestinationFile;
    }

    private readonly SyncBll syncBll = CarApplication.Instance.DestinationSyncBll;

  }
}