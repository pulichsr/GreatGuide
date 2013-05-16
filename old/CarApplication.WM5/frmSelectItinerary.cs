using System.Windows.Forms;
using Nucleo.GoodGuide.Types;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectItinerary : Form
  {
    public static DialogResult Select(ItineraryFiles itineraryFiles,out ItineraryFile selectedFile)
    {
      selectedFile = null;

      frmSelectItinerary frm = new frmSelectItinerary(itineraryFiles);
      DialogResult result = frm.ShowDialog();
      if (result == DialogResult.Cancel)
        return result;

      selectedFile = frm.SelectedRow;

      return result;
    }

    private frmSelectItinerary(ItineraryFiles itineraryFiles)
    {
      InitializeComponent();

      LoadResources();

      SmartGrid.DefaultRowHeight = 25;

      DataSource = itineraryFiles;
    }

    protected object DataSource
    {
      set
      {
        sgData.DataSource = value;
        if (sgData.Rows.Count > 0)
        {
          sgData.ActivateCell(0, 0);
          btnUp.Visible = true;
          btnDown.Visible = true;
        }
        else
        {
          btnUp.Visible = false;
          btnDown.Visible = false;
        }
      }
    }
    protected ItineraryFile SelectedRow
    {
      get
      {
        if (this.BindingContext[sgData.DataSource].Position == -1)
          return null;

        return (ItineraryFile)this.BindingContext[sgData.DataSource].Current;
      }
    }

    private void OkClicked()
    {

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private void LoadResources()
    {
      btnBack.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgBack;
      btnUp.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListUp;
      btnDown.ActiveBackgroundImage = CarApplication.Instance.ImageManager.ImgListDown;
    }

    private void btnOK_Click(object sender, System.EventArgs e)
    {
      OkClicked();
    }
    private void btnUp_MouseUp(object sender, MouseEventArgs e)
    {
      if (sgData.ActiveRowIndex == 0)
        return;

      sgData.ActivateCell(sgData.ActiveRowIndex - 1, 0);
    }
    private void btnDown_MouseUp(object sender, MouseEventArgs e)
    {
      if (sgData.ActiveRowIndex == sgData.Rows.Count - 1)
        return;

      sgData.ActivateCell(sgData.ActiveRowIndex + 1, 0);
    }
    private void btnBack_Click(object sender, System.EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
    }
  }
}