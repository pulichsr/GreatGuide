using System;
using System.Windows.Forms;
using DataObjects=Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmConfirmRegion : Form
  {
    public static DialogResult Confirm(DataObjects.Region region)
    {
      frmConfirmRegion frm = new frmConfirmRegion(region);
      DialogResult result = frm.ShowDialog();
      frm.Dispose();

      return result;
    }

    private frmConfirmRegion(DataObjects.Region region)
    {
      Guard.ArgumentNotNull(region, "region");

      InitializeComponent();
      LoadResources();

      this.region = region;

      lblRegion.DataBindings.Add("Text", region, "CollatedName", true, DataSourceUpdateMode.Never);
    }

    private void LoadResources()
    {
///*
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnNext.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgNext;
//*/
    }

    private void btnBack_Click(object sender, EventArgs e)
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    private void btnNext_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
    }


    #region Fields
    private readonly DataObjects.Region region;
    #endregion

  }
}