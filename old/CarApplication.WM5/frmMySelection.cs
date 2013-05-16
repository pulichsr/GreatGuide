using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmMySelection : frmDestinationListBase
  {
    public frmMySelection()
    {
      InitializeComponent();

      try
      {
        LoadResources();
      }
      catch
      {
      }
    }

    public override void BuildPresentation(Windows.Forms.DynamicForms.FormDefinition formDefinition, object newFormData)
    {
      base.BuildPresentation(formDefinition, newFormData);

      BtnSoftKey1.Text = "SEARCH";
      BtnSoftKey2.Text = "VIEW|MAP";
    }

    protected override DestinationDataset.DestinationDataTable GetData()
    {
      try
      {
        return CarApplication.Instance.DestinationBll.GetMySelection();
      }
      catch
      {
        return null;
      }
    }
    protected override void RowMoreClicked()
    {
      if (SelectedRow == null)
        return;

      MoreResults Result = frmDestinationMore.ShowMore(SelectedRow,false);
      if ((Result == MoreResults.TakeMeThere) || (Result == MoreResults.ViewMap))
        Close();
    }
    protected override void RowSelected()
    {
      BtnSoftKey1.Enabled = true;

      if (DestinationBll.CanRouteTo(SelectedRow) == true)
      {
//        BtnSoftKey2.Enabled = true;
        BtnSoftKey3.Enabled = true;
      }
      else
      {
        BtnSoftKey2.Enabled = false;
        BtnSoftKey3.Enabled = false;
      }
    }

    private void LoadResources()
    {
      LoadTemplate(string.Format("Nucleo.GoodGuide.CarApplicationResources.Templates.{0}", GetType().Name));
    }

    #region Event handlers
    #endregion
  }
}