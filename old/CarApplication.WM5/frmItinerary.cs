using System;
using System.Windows.Forms;
using Nucleo.GoodGuide.Bll;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types;
using Nucleo.WinCe;
using Nucleo.Xml;

namespace Nucleo.GoodGuide.CarApplication
{
  public partial class frmItinerary : frmDestinationListBase
  {
    public frmItinerary():
      this(true)
    {
    }
    public frmItinerary(Boolean isManaged):
      base(isManaged)
    {
      InitializeComponent();

      try
      {
        LoadResources();
      }
      catch(Exception exc)
      {
        CarApplication.Instance.WriteLog(this,"Error loading resources",exc);
      }
    }

    public override void BuildPresentation(Windows.Forms.DynamicForms.FormDefinition formDefinition, object newFormData)
    {
      base.BuildPresentation(formDefinition, newFormData);

      BtnSoftKey1.Text = "PREVIOUS|DAY";
      BtnSoftKey2.Text = "NEXT|DAY";

      if (newFormData == null)
        return;

      if (newFormData is string == false)
        return;

      // Prepend XML preamble for deserialization
      string FormData = string.Format("<?xml version=\"1.0\" encoding=\"UTF-8\"?>{0}", newFormData);

      try
      {
        if (FormData != string.Empty)
          formDataObject = XmlSerialization.DeserializeFromString<ItineraryFormData>(FormData);
      }
      catch
      {
      }

      if (formDataObject == null)
        return;
    }

    protected override void NavigationControlClicked(Control navigationControl, ref bool handled, ref object clickData)
    {
      CarApplication.Instance.WriteLog(this, ">>NavigationControlClicked");
      base.NavigationControlClicked(navigationControl,ref handled,ref clickData);
      if (handled == true)
      {
        return;
      }

      if (navigationControl == BtnBack)
      {
        WinCe.WaitCursor.Show(true);

        handled = false;
        return;
      }
      if (navigationControl == BtnMap)
      {
        WinCe.WaitCursor.Show(true);

        handled = false;
        return;
      }
      if (navigationControl == BtnSoftKey1)
      {
        PreviousDay();
        handled = true;
        return;
      }
      if (navigationControl == BtnSoftKey2)
      {
        NextDay();
        handled = true;
        return;
      }
    }
    protected override DestinationDataset.DestinationDataTable GetData()
    {
      CarApplication.Instance.WriteLog(this, ">> GetData");
      DestinationDataset.DestinationDataTable Destinations;

      if (CarApplication.Instance.IsGpsTimeSet == true)
      {
        // If GPS date was set, then get itinerary for the current date
        CarApplication.Instance.WriteLog(this, "  CarApplication.Instance.IsGpsTimeSet == true");

        #region If GPS time is available, get current date
        // Get local time
        Nucleo.WinCe.System.SYSTEMTIME localTime = new Nucleo.WinCe.System.SYSTEMTIME();
        try
        {
          Nucleo.WinCe.System.GetLocalTime(ref localTime);
        }
        catch
        {
        }

        // Set day number
        DateTime LocalDate = new DateTime(localTime.wYear, localTime.wMonth, localTime.wDay);
        #endregion

        if (CarApplication.Instance.Itinerary != null)
        {
          CarApplication.Instance.WriteLog(this, "CarApplication.Instance.Itinerary != null");
          if ((CarApplication.Instance.Itinerary.IsArrivalDatNull() == false) && (LocalDate < CarApplication.Instance.Itinerary.ArrivalDat))
            LocalDate = CarApplication.Instance.Itinerary.ArrivalDat;

          if ((CarApplication.Instance.Itinerary.IsDepartureDatNull() == false) && (LocalDate > CarApplication.Instance.Itinerary.DepartureDat))
            LocalDate = CarApplication.Instance.Itinerary.DepartureDat;
        }

        CarApplication.Instance.WriteLog(this, string.Format("Getting itinerary for {0}", LocalDate.ToString("yyyy-MM-dd")));
        Destinations = GetDayData(LocalDate);
        lblDay.Text = string.Format("Day {0}/{1}", dayNumber, numberOfDays);
      }
      else
      {
        // If GPS date was NOT set, then get itinerary for day 1
        CarApplication.Instance.WriteLog(this, "  CarApplication.Instance.IsGpsTimeSet == false");
        dayNumber = 1;
        Destinations = GetDayData(dayNumber);
        lblDay.Text = string.Format("Day {0}/{1}", dayNumber, numberOfDays);
      }

      return Destinations;
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
    protected override void UpdateControls()
    {
      base.UpdateControls();

      BtnSoftKey1.Enabled = dayNumber > 1;
      BtnSoftKey2.Enabled = dayNumber < numberOfDays;
    }
    protected override void RowSelected()
    {
      if (SelectedRow == null)
        return;

      if (DestinationBll.CanRouteTo(SelectedRow) == true)
        BtnSoftKey3.Enabled = true;
      else
        BtnSoftKey3.Enabled = false;
    }

    private Int16 DayNumber
    {
      get { return dayNumber; }
      set
      {
        if ((value < 1) || (value > numberOfDays))
          return;

        dayNumber = value;
        FirstPage();
        DataSource = GetDayData(dayNumber);

        lblDay.Text = string.Format("Day {0}/{1}", dayNumber, numberOfDays);
      }
    }
    private DateTime ItineraryDate
    {
      set { lblDate.Text = value.ToString("ddd dd MMM yyyy"); }
    }

    private void LoadResources()
    {
      LoadTemplate(string.Format("Nucleo.GoodGuide.CarApplicationResources.Templates.{0}", GetType().Name));
    }
    private DestinationDataset.DestinationDataTable GetDayData(DateTime dateToGet)
    {
      CarApplication.Instance.WriteLog(this, string.Format(">> GetDayData({0})", dateToGet));
      DestinationDataset.DestinationDataTable Destinations = null;

      try
      {
        Destinations = CarApplication.Instance.DestinationBll.GetByItineraryDate(dateToGet, out dayNumber, out numberOfDays, out comment);
        if (Destinations == null)
          return null;

        CarApplication.Instance.WriteLog(this, string.Format("{0} destinations", Destinations.Count));
        CarApplication.Instance.WriteLog(this, string.Format("   Comment:{0}", comment));
        Comment = comment;
        ItineraryDate = dateToGet;
      }
      catch
      {
      }

      return Destinations;
    }
    private DestinationDataset.DestinationDataTable GetDayData(Int16 dayNumberToGet)
    {
      CarApplication.Instance.WriteLog(this,string.Format(">> GetDayData({0})",dayNumberToGet));
      DestinationDataset.DestinationDataTable Destinations = null;

      try
      {
        Destinations = CarApplication.Instance.DestinationBll.GetByItineraryDayNumber(dayNumberToGet, out itineraryDate, out numberOfDays, out comment);
        if (Destinations == null)
        {
          CarApplication.Instance.WriteLog(this, "No destinations");
          return null;
        }

        Comment = comment;
        ItineraryDate = itineraryDate;
      }
      catch(Exception exc)
      {
        CarApplication.Instance.WriteLog(this, "Error getting destinations",exc);
      }

      return Destinations;
    }
    private void PreviousDay()
    {
      DayNumber = (Int16)(dayNumber - 1);
    }
    private void NextDay()
    {
      DayNumber = (Int16)(dayNumber + 1);
    }

    #region Event handlers
    private void frmItinerary_Load(object sender, EventArgs e)
    {
    }
    private void frmItinerary_Activated(object sender, EventArgs e)
    {
      WaitCursor.Show(false);
    }
    #endregion

    private ItineraryFormData formDataObject = null;
    private DateTime itineraryDate;
    private Int16 numberOfDays;
    private Int16 dayNumber;
    private string comment;
    private Boolean firstDayOnly = false;


  }

  [Serializable]
  public class ItineraryFormData
  {
    public ItineraryFormData()
    {
    }
    public ItineraryFormData(DateTime date)
    {
      this.date = date;
    }

    public DateTime? Date
    {
      get { return date; }
      set { date = value; }
    }

    private DateTime? date;
  }

}