using System.Windows.Forms;
using Nucleo.GoodGuide.Controls;
using Nucleo.GoodGuide.Datasets.Datasets;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectPoiDestination : frmCarApplicationBase
  {
    public static DialogResult Select(DestinationDataset.DestinationDataTable destinations ,out DestinationDataset.DestinationRow selectedDestination)
    {
      frmSelectPoiDestination frm = new frmSelectPoiDestination(destinations);
      DialogResult Result = frm.ShowDialog();

      selectedDestination = frm.selectedDestination;    
  
      return Result;
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled,ref clickData);

      if (navigationControl == BtnBack)
      {
        handled = true;
        DialogResult = System.Windows.Forms.DialogResult.Cancel;        
      }

      if (navigationControl == BtnSoftKey3)
      {
        selectedDestination = (DestinationDataset.DestinationRow)(((System.Data.DataRowView)(BindingContext[sgData.DataSource].Current)).Row);
        DialogResult = System.Windows.Forms.DialogResult.OK;

        handled = true;
        return;
      }

      if (navigationControl == BtnSoftKey4)
      {
        pageController.PreviousPage();

        handled = true;
        return;
      }

      if (navigationControl == BtnSoftKey5)
      {
        pageController.NextPage();

        handled = true;
        return;
      }
    }

    private frmSelectPoiDestination(DestinationDataset.DestinationDataTable destinations)
    {
      InitializeComponent();

      SmartGrid.DefaultRowHeight = 25;
      BtnMap.Visible = false;

      pageController.DataSource = destinations;
      pageController.PageNumberChanged += pageController_PageNumberChanged;
      sgData.DataSource = pageController.PageData;

      BtnSoftKey3.Text = "TAKE ME|THERE";
      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";

      if (destinations.Count == 0)
      {
        BtnSoftKey3.Enabled = false;
        BtnSoftKey4.Enabled = false;
        BtnSoftKey5.Enabled = false;
      }
      else
      {
        sgData.ActivateCell(0, 0);
        BtnSoftKey3.Enabled = true;
      }

      UpdateControls();
    }

    private void UpdateControls()
    {
      BtnSoftKey4.Enabled = pageController.PageNumber > 1;
      BtnSoftKey5.Enabled = pageController.PageNumber < pageController.PageCount;
    }

    private void pageController_PageNumberChanged(object sender, System.EventArgs e)
    {
      sgData.DataSource = pageController.PageData;
      UpdateControls();
    }
    
    private readonly DestinationDataset.DestinationDataTable destinations = null;
    private DestinationDataset.DestinationRow selectedDestination = null;
    private readonly ListPageController pageController = new ListPageController(7);
  }
}