using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectPoiCategory : frmSelectPoiBase
  {
    public static DialogResult Select(PoiCategories categories,out PoiCategory selectedCategory)
    {
      frmSelectPoiCategory frm = new frmSelectPoiCategory(categories);
      DialogResult Result = frm.ShowDialog();

      selectedCategory = frm.selectedCategory;      

      return Result;
    }

    private frmSelectPoiCategory(PoiCategories categories)
    {
      InitializeComponent();

      Datasource = categories;
    }

    protected override void BackClicked()
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    protected override void ContinueClicked()
    {
      selectedCategory = (PoiCategory)SelectedRow;

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }

    private PoiCategory selectedCategory = null;
  }
}