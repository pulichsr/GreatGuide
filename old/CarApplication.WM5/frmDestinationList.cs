using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmDestinationList : frmDestinationListBase
  {
    public frmDestinationList()
    {
      CarApplication.Instance.WriteLog(this, "frmDestinationList.ctor");
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

      BtnSoftKey1.Enabled = true;
      BtnSoftKey1.Text = "SEARCH";
      BtnSoftKey2.Text = "VIEW|MAP";

      if (newFormData == null)
        return;

      if (newFormData is string == false)
        return;

      // Prepend XML preamble for deserialization
      string FormData = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>{0}", newFormData);

      try
      {
        if (FormData != string.Empty)
          formDataObject = XmlSerialization.DeserializeFromString<DestinationListFormData>(FormData);
      }
      catch
      {
      }

      if (formDataObject == null)
        return;

      if (formDataObject.Comment != null)
        Comment = formDataObject.Comment;
    }

    protected override DestinationDataset.DestinationDataTable GetData()
    {
      if (formDataObject == null)
        return null;

      try
      {
        destinations = CarApplication.Instance.DestinationBll.GetByDataObject(formDataObject);
      }
      catch(Exception exc)
      {
        CarApplication.Instance.WriteLog(this,ExceptionManager.MessageWithStackTrace("Error getting destinations",exc));
        return null;
      }


      GpsPositionEvent Position = CarApplication.Instance.CurrentGpsPosition;
      if (Position != null)
      {
        CarApplication.Instance.DestinationBll.CalculateDistanceDirection(destinations, Position.Latitude,Position.Longitude,CarApplication.Instance.Units);
        CarApplication.Instance.DestinationBll.Sort(destinations, "Distance");
      }

      return destinations;
    }
    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      base.NavigationControlClicked(navigationControl, ref handled,ref clickData);
      if (handled == true)
        return;

      if (navigationControl == BtnSoftKey1)
      {
        CarApplication.Instance.LoadNewFormData();
        DestinationDataset.DestinationRow Destination;
        DialogResult Result = frmDestinationListSearch.Search(destinations, out Destination);
        
        destinations.DefaultView.RowFilter = string.Empty;

        if (Result == System.Windows.Forms.DialogResult.OK)
        {
          MoreResults MoreResult = frmDestinationMore.ShowMore(Destination, false);
          if ((MoreResult == MoreResults.TakeMeThere) || (MoreResult == MoreResults.ViewMap))
            Close();
        }

        handled = true;
        return;
      }
    }
    protected override void HeaderMoreClicked()
    {
      MoreResults Result = frmCommentMore.ShowDetail(Comment);
      if ((Result == MoreResults.TakeMeThere) || (Result == MoreResults.ViewMap))
        Close();
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
      if (DestinationBll.CanRouteTo(SelectedRow) == true)
      {
        BtnSoftKey3.Enabled = true;
      }
      else
      {
        BtnSoftKey3.Enabled = false;
      }
    }

    private void LoadResources()
    {
      LoadTemplate(string.Format("Nucleo.GoodGuide.CarApplicationResources.Templates.{0}", GetType().Name));
    }

    private void frmDestinationList_Activated(object sender, EventArgs e)
    {
      Nucleo.WinCe.WaitCursor.Show(false);
    }
    private void frmDestinationList_Load(object sender, EventArgs e)
    {
    }

    private DestinationListFormData formDataObject = null;
    private DestinationDataset.DestinationDataTable destinations = null;

  }
}