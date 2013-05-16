using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.Windows.Forms.DynamicForms;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmLoadFormDefinition : Form
  {
    public static void ShowForm()
    {
      frmLoadFormDefinition frm = new frmLoadFormDefinition();
      frm.ShowDialog();
      frm.Dispose();
    }

    internal frmLoadFormDefinition()
    {
      InitializeComponent();
    }

    private void SyncStep(object sender,XmlDataSetEventArgs e)
    {
      lblStep.Text = e.Text;
      System.Windows.Forms.Application.DoEvents();
    }

    private void frmLoadFormDefinition_Load(object sender, EventArgs e)
    {
      edtCurrentFile.Text = CarApplication.Instance.LoadedFormDefinitionFile;

      syncBll.SyncStep += SyncStep;
    }
    private void frmLoadFormDefinition_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
        CarApplication.Instance.FormDefinitionBll.BuildDefinitions(DynamicFormManager.FormDefinitions);
      }
      catch (Exception exc)
      {
        frmFactoryError.ShowException(exc);
        return;
      }

      CarApplication.Instance.LoadedFormDefinitionFile = System.IO.Path.GetFileNameWithoutExtension(edtFilename.Text);
      edtCurrentFile.Text = CarApplication.Instance.LoadedFormDefinitionFile;
    }

    private readonly SyncBll syncBll = CarApplication.Instance.FormDefinitionSyncBll;

  }
}