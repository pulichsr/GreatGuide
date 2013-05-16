using System.Windows.Forms;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmSelectPoiSubcategory : frmSelectPoiBase
  {
    public static DialogResult Select(PoiSubcategories subcategories,out PoiSubcategory selectedSubcategory)
    {
      frmSelectPoiSubcategory frm = new frmSelectPoiSubcategory(subcategories);
      DialogResult Result = frm.ShowDialog();

      selectedSubcategory = frm.selectedSubcategory;

      return Result;
    }

    private frmSelectPoiSubcategory(PoiSubcategories subcategories)
    {
      InitializeComponent();

      BtnMap.Visible = false;
      Datasource = subcategories;
    }

    protected override void BackClicked()
    {
      DialogResult = System.Windows.Forms.DialogResult.Cancel;
    }
    protected override void ContinueClicked()
    {
      selectedSubcategory = (PoiSubcategory)SelectedRow;

      DialogResult = System.Windows.Forms.DialogResult.OK;
    }
   
    private PoiSubcategory selectedSubcategory = null;
  }
}