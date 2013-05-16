using System;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.CarApplication
{
  internal static class TestData
  {
    public static void Insert(IRepositoryLocator locator)
    {
      try
      {
        InsertConfiguration(locator);
        InsertMasterArea(locator);
        InsertDestinationType(locator);
        InsertFormDefinition(locator);
        InsertItinerary(locator);
        InsertItineraryDay(locator);
      }
      catch (Exception exc)
      {
        throw new ApplicationException("Error inserting test data");
      }
    }

    private static void InsertConfiguration(IRepositoryLocator locator)
    {
      INamedParameterRepository repository = locator.LocateRepository<INamedParameterRepository>();
      if (repository == null)
        throw new InvalidOperationException("INamedParameterRepository not found");

      repository.SetBoolean("IsLoggingActive",true);
      repository.SetBoolean("IsExceptionLoggingActive", true);
      repository.SetInt32("MasterAreaId",1);
      repository.SetBoolean("FirstUse", false);
    }
    private static void InsertMasterArea(IRepositoryLocator locator)
    {
      IMasterAreaRepository repository = locator.LocateRepository<IMasterAreaRepository>();
      if (repository == null)
        throw new InvalidOperationException("IMasterAreaRepository not found");

      MasterAreaDataset dataset = new MasterAreaDataset();
      MasterAreaDataset.MasterAreaRow row = dataset.MasterArea.NewMasterAreaRow();

      row.ContentBasePath = string.Empty;
      row.Description = "Description";
      row.Id = 1;
      row.MaxLatitude = 0;
      row.MaxLongitude = 0;
      row.MinLatitude = 0;
      row.MinLongitude = 0;
      row.Name = "MasterArea 1";
      row.RegionData = string.Empty;
      repository.Insert(row);
    }
    private static void InsertDestinationType(IRepositoryLocator locator)
    {
      IDestinationTypeRepository repository = locator.LocateRepository<IDestinationTypeRepository>();
      if (repository == null)
        throw new InvalidOperationException("IDestinationTypeRepository not found");

      DestinationTypeDataset dataset = new DestinationTypeDataset();
      DestinationTypeDataset.DestinationTypeRow row = dataset.DestinationType.NewDestinationTypeRow();

      row.sCode = "DSTT 1";
      row.Comment1Label = "Comment 1";
      row.Comment2Label = "Comment 2";
      row.Comment3Label = "Comment 3";
      row.Comment4Label = "Comment 4";
      row.Description = "Description";
      row.IconResourceName = "BED";
      row.Id = 1;
      row.Name = "Destination Type 1";
      repository.Insert(row);
    }
    private static void InsertFormDefinition(IRepositoryLocator locator)
    {
      IFormDefinitionRepository repository = locator.LocateRepository<IFormDefinitionRepository>();
      if (repository == null)
        throw new InvalidOperationException("IFormDefinitionRepository not found");

      FormDefinitionDataset dataset = new FormDefinitionDataset();
      FormDefinitionDataset.FormDefinitionRow row = dataset.FormDefinition.NewFormDefinitionRow();

      row.FormTypeName = typeof(frmWelcome).FullName;
      row.GraphicResourceName = "";
      row.Id = 1;
      row.MasterAreaId = 1;
      row.Name = "WelcomeForm";
      row.Text = "Text";
      repository.Insert(row);

      row.FormTypeName = typeof(frmEightButton).FullName;
      row.GraphicResourceName = "";
      row.Id = 2;
      row.MasterAreaId = 1;
      row.Name = "MainMenuForm";
      row.Text = "Text";
      repository.Insert(row);

      row.FormTypeName = typeof(frmDisclaimer).FullName;
      row.GraphicResourceName = "";
      row.Id = 3;
      row.MasterAreaId = 1;
      row.Name = "DisclaimerForm";
      row.Text = "Text";
      repository.Insert(row);
    
    }
    private static void InsertControlDefinition(IRepositoryLocator locator)
    {
      IControlDefinitionRepository repository = locator.LocateRepository<IControlDefinitionRepository>();
      if (repository == null)
      throw new InvalidOperationException("IControlDefinitionRepository not found");

      ControlDefinitionDataset dataset = new ControlDefinitionDataset();
      ControlDefinitionDataset.ControlDefinitionRow row = dataset.ControlDefinition.NewControlDefinitionRow();

      row.Action = "";
      row.ActionData = "";
      row.ActionTarget = "";
//      row.Colour = ColorUtils.;
      row.Description = "";
      row.FormId = 1;
      row.GraphicResourceName = "";
      row.Id = 1;
      row.MasterAreaId = 1;
      row.Name = "btnBack";
      row.Text = "Text";
      repository.Insert(row);

    }
    private static void InsertItinerary(IRepositoryLocator locator)
    {
      IItineraryRepository repository = locator.LocateRepository<IItineraryRepository>();
      if (repository == null)
        throw new InvalidOperationException("IItineraryRepository not found");

      ItineraryDataset dataset = new ItineraryDataset();
      ItineraryDataset.ItineraryRow row = dataset.Itinerary.NewItineraryRow();

      row.ArrivalDat = DateTime.Today.AddDays(-1);
      row.BookingReference = "";
      row.Branding1 = "";
      row.Branding2 = "";
      row.Branding3 = "";
      row.DepartureDat = DateTime.Today.AddDays(10);
      row.FirstName = "First";
      row.GeofenceData = "";
      row.GracePeriod = 10;
      row.Id = 1;
      row.LastName = "Last";
      row.Title = "Mr.";
      repository.Insert(row);
    }
    private static void InsertItineraryDay(IRepositoryLocator locator)
    {
      IItineraryDayRepository repository = locator.LocateRepository<IItineraryDayRepository>();
      if (repository == null)
        throw new InvalidOperationException("IItineraryDayRepository not found");

      ItineraryDayDataset dataset = new ItineraryDayDataset();
      ItineraryDayDataset.ItineraryDayRow row = dataset.ItineraryDay.NewItineraryDayRow();

      row.Comment = "Comment Day 1";
      row.Id = 1;
      row.ItineraryDat = DateTime.Today.AddDays(-1);
      repository.Insert(row);

      row.Comment = "Comment Day 2";
      row.Id = 2;
      row.ItineraryDat = DateTime.Today;
      repository.Insert(row);

      row.Comment = "Comment Day 3";
      row.Id = 3;
      row.ItineraryDat = DateTime.Today.AddDays(1);
      repository.Insert(row);
    }
  }
}

