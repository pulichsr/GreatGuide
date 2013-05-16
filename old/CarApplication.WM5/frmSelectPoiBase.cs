using System.Windows.Forms;
using Nucleo.GoodGuide.Controls;
using Resco.Controls.SmartGrid;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectPoiBase : frmCarApplicationBase
  {
    protected frmSelectPoiBase()
    {
      InitializeComponent();

      BtnMap.Visible = false;

      SmartGrid.DefaultRowHeight = 25;

      pageController.PageNumberChanged += pageController_PageNumberChanged;

      BtnSoftKey3.Text = "CONTINUE";
      BtnSoftKey4.Text = "PREVIOUS|PAGE";
      BtnSoftKey5.Text = "NEXT|PAGE";
    }

    protected object Datasource
    {
      set
      {
        pageController.DataSource = value;
        sgData.DataSource = pageController.PageData;

        if (pageController.PageData.Count == 0)
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
    }
    protected object SelectedRow
    {
      get { return BindingContext[sgData.DataSource].Current; }
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled,ref clickData);

      if (navigationControl == BtnBack)
      {
        handled = true;

        BackClicked();
      }

      if (navigationControl == BtnSoftKey3)
      {
        handled = true;

        ContinueClicked();
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

    protected virtual void BackClicked()
    {
      
    }
    protected virtual void ContinueClicked()
    {

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

    private readonly ListPageController pageController = new ListPageController(7);
  }
}