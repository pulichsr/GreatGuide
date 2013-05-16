using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Types.DataObjects;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmConfirmStreet : Form
  {
    public static DialogResult Confirm(Street street)
    {
      frmConfirmStreet frm = new frmConfirmStreet(street);
      DialogResult result = frm.ShowDialog();
      frm.Dispose();

      return result;
    }

    private frmConfirmStreet(Street street)
    {
      Guard.ArgumentNotNull(street, "street");

      InitializeComponent();
      LoadResources();

      this.street = street;

      lblStreet.DataBindings.Add("Text", street, "Name", true, DataSourceUpdateMode.Never);
      lblRegion.DataBindings.Add("Text", street, "RegionCollatedName", true, DataSourceUpdateMode.Never);
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
    private readonly Street street;
    #endregion

  }
}