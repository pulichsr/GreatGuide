using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Datasets.XmlDatasets;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces;
using Nucleo.GoodGuide.Types.Interfaces.Dal;

namespace Nucleo.GoodGuide.Bll
{
  public class DestinationBll : ISyncTarget
  {
    public const Int16 DefaultArrivalZoneRadius = 30;
    public const Int32 SearchKeyWidth = 10;

    public DestinationBll(
      ILogger logger,
      IDestinationRepository destinationRepository,
      IDestinationTypeRepository destinationTypeRepository,
      IDestinationClassificationRepository destinationClassificationRepository,
      IDestinationCollectionRepository destinationCollectionRepository, 
      ItineraryDayBll itineraryDayBll)
    {
      Guard.ArgumentNotNull(logger, "logger");
      Guard.ArgumentNotNull(destinationRepository, "destinationRepository");
      Guard.ArgumentNotNull(destinationTypeRepository, "destinationTypeRepository");
      Guard.ArgumentNotNull(destinationClassificationRepository, "destinationClassificationRepository");
      Guard.ArgumentNotNull(destinationCollectionRepository, "destinationCollectionRepository");
      Guard.ArgumentNotNull(itineraryDayBll, "itineraryDayBll");

      this.logger = logger;
      this.destinationRepository = destinationRepository;
      this.destinationTypeRepository = destinationTypeRepository;
      this.destinationClassificationRepository = destinationClassificationRepository;
      this.destinationCollectionRepository = destinationCollectionRepository;
      this.itineraryDayBll = itineraryDayBll;
      this.imageMapper = imageMapper;
    }

    public int MasterAreaId
    {
      get { return masterAreaId; }
      set { masterAreaId = value; }
    }

    #region ISyncTarget
    public string DatasetName
    {
      get { return "Destination"; }
    }
    public void Insert(GetDataResponse newData, Int32 offset, Boolean merge)
    {
      logger.Write(this, ">> ISyncTarget.Insert");
      DestinationDataset.DestinationDataTable Data = (DestinationDataset.DestinationDataTable)newData.Tables["Destination"];
      if (Data != null)
      {
        if (merge == false)
        {
          logger.Write(this,"   Not merging: DeleteAll");
          DeleteAll();
        }

        for (Int32 RowNo = 0; RowNo < Data.Rows.Count; RowNo++)
        {
          Data[RowNo].Id += offset;

          if(Data[RowNo].IsClassificationIdNull() == false) Data[RowNo].ClassificationId += offset;
          if (Data[RowNo].IsCollectionIdNull() == false) Data[RowNo].CollectionId += offset;
          if (Data[RowNo].IsDestinationTypeIdNull() == false) Data[RowNo].DestinationTypeId += offset;
          if (Data[RowNo].IsMasterAreaIdNull() == false) Data[RowNo].MasterAreaId += offset;
          if (Data[RowNo].IsThemeIdNull() == false) Data[RowNo].ThemeId += offset;

          try
          {
            logger.Write(this, string.Format("   Inserting destination {0}:{1}", Data[RowNo].Id, Data[RowNo].Code));
            Insert(Data[RowNo]);
          }
          catch (Exception exc)
          {
            logger.Write(this,"   Error inserting destination",exc);
          }
        }

        Data.Dispose();
      }
    }
    public void DeleteAll()
    {
      destinationRepository.DeleteAll();
    }
    #endregion

    public void SyncItineraryData(GetDataResponse newData)
    {
      DestinationDataset.DestinationDataTable NewDestinations = (DestinationDataset.DestinationDataTable)newData.Tables["Destination"];
      if (NewDestinations != null)
      {
        for (Int32 RowNo = 0; RowNo < NewDestinations.Rows.Count; RowNo++)
        {
          DestinationDataset.DestinationRow CurrentDestination = destinationRepository.GetById(NewDestinations[RowNo].Id);
          if ((CurrentDestination != null) && (NewDestinations[RowNo].VersionNo > CurrentDestination.VersionNo))
              destinationRepository.DeleteById(CurrentDestination.Id);

          Insert(NewDestinations[RowNo]);
        }

        NewDestinations.Dispose();
      }
    }

    public void CalculateDistanceDirection(DestinationDataset.DestinationDataTable destinations, float latitude, float longitude,Units units)
    {
      if (destinations == null)
        return;

      if (destinations.Columns["Distance"] == null)
        return;

      for (Int32 DestinationNo = 0; DestinationNo < destinations.Rows.Count; DestinationNo++)
      {
        DestinationDataset.DestinationRow Destination = destinations[DestinationNo];
        if ((Destination.IsLatitudeNull() == true) || (Destination.IsLongitudeNull() == true))
        {
          Destination["Distance"] = Int32.MaxValue;
          continue;
        }

        Destination["Distance"] = Nucleo.Math.Geo.GeoCalculations.ApproximateDistance(
            latitude, longitude, Destination.Latitude, Destination.Longitude);
        Destination["DistanceText"] = DistanceFormatter.Format((Int32)Destination["Distance"],units);

        Destination["Direction"] = Nucleo.Math.Geo.GeoCalculations.ApproximateBearing(
            latitude, longitude, Destination.Latitude, Destination.Longitude);
        Destination["DirectionImageIndex"] = Direction.FromHeading((Int16)Destination["Direction"]);
        Destination["DirectionText"] = Direction.ToShortString((Int16)Destination["DirectionImageIndex"]);
      }
    }
    public void Sort(DestinationDataset.DestinationDataTable destinations,string sortKey)
    {
      if (destinations == null)
        return;
      if (destinations.Count == 0)
        return;

      destinations.DefaultView.Sort = sortKey;
    }
    public void AddDisplayColumns(DestinationDataset.DestinationDataTable destinations)
    {
      if (destinations == null)
        return;

      destinations.Columns.Add("TypeImageIndex", typeof(Int16));
      destinations.Columns.Add("RecommendationText", typeof(string));
      destinations.Columns.Add("Distance", typeof(Int32));
      destinations.Columns.Add("DistanceText", typeof(string));
      destinations.Columns.Add("Direction", typeof(Int16));
      destinations.Columns.Add("DirectionImageIndex", typeof(Int16));
      destinations.Columns.Add("DirectionText", typeof(string));
    }

    public DestinationDataset.DestinationRow GetById(Int32 id)
    {
      return destinationRepository.GetById(id);
    }
    public DestinationDataset.DestinationDataTable GetAll(Int32 masterAreaIdToGet)
    {
      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetAll(masterAreaIdToGet);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByTypeCode(Int32 masterAreaIdToGet,string destinationTypeCode)
    {
      DestinationTypeDataset.DestinationTypeRow DestinationType = destinationTypeRepository.GetByCode(destinationTypeCode);
      if (DestinationType == null)
        return new DestinationDataset.DestinationDataTable();

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByType(masterAreaIdToGet,DestinationType.Id);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByClassificationCode(Int32 masterAreaIdToGet,string classificationCode)
    {
      DestinationClassificationDataset.DestinationClassificationRow DestinationClassification = destinationClassificationRepository.GetByCode(classificationCode);
      if (DestinationClassification == null) 
        return new DestinationDataset.DestinationDataTable();

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByClassification(masterAreaIdToGet,DestinationClassification.Id);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByCollection(Int32 masterAreaIdToGet,string collectionCode)
    {
      DestinationCollectionDataset.DestinationCollectionRow DestinationCollection = destinationCollectionRepository.GetByCode(collectionCode);
      if (DestinationCollection == null)
      {
        return new DestinationDataset.DestinationDataTable();
      }

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByCollection(masterAreaIdToGet,DestinationCollection.Id);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByClassificationCollection(Int32 masterAreaIdToGet,string classificationCode, string collectionCode)
    {
      DestinationCollectionDataset.DestinationCollectionRow DestinationCollection = destinationCollectionRepository.GetByCode(collectionCode);
      if (DestinationCollection == null)
        return new DestinationDataset.DestinationDataTable();

      DestinationClassificationDataset.DestinationClassificationRow DestinationClassification = destinationClassificationRepository.GetByCode(classificationCode);
      if (DestinationClassification == null)
        return new DestinationDataset.DestinationDataTable();

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByClassificationCollection(masterAreaIdToGet,DestinationClassification.Id, DestinationCollection.Id);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByTypeCollection(Int32 masterAreaIdToGet,string typeCode, string collectionCode)
    {
      DestinationCollectionDataset.DestinationCollectionRow DestinationCollection = destinationCollectionRepository.GetByCode(collectionCode);
      if (DestinationCollection == null)
      {
        return new DestinationDataset.DestinationDataTable();
      }

      DestinationTypeDataset.DestinationTypeRow DestinationType = destinationTypeRepository.GetByCode(typeCode);
      if (DestinationType == null)
      {
        return new DestinationDataset.DestinationDataTable();
      }

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByTypeCollection(masterAreaIdToGet,DestinationType.Id, DestinationCollection.Id);

      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByItineraryDate(DateTime date,out Int16 dayNumber,out Int16 numberOfDays,out string comment)
    {
      comment = string.Empty;

      ItineraryDayDataset.ItineraryDayRow ItineraryDay = itineraryDayBll.GetByDate(date,out dayNumber,out numberOfDays);
      if (ItineraryDay == null)
        return new DestinationDataset.DestinationDataTable();

      comment = ItineraryDay.Comment;

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByItineraryDayId(ItineraryDay.Id);
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByItineraryDayNumber(Int16 dayNumber, out DateTime date, out Int16 numberOfDays, out string comment)
    {
      comment = string.Empty;

      ItineraryDayDataset.ItineraryDayRow ItineraryDay = itineraryDayBll.GetByDayNumber(dayNumber, out date, out numberOfDays);
      if (ItineraryDay == null)
      {
        return new DestinationDataset.DestinationDataTable();
      }

      comment = ItineraryDay.sComment;

      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetByItineraryDayId(ItineraryDay.Id);
      if (Destinations == null)
        return null;

      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetMySelection()
    {
      DestinationDataset.DestinationDataTable Destinations = destinationRepository.GetMySelection();
      AddDisplayColumns(Destinations);

      return Destinations;
    }
    public DestinationDataset.DestinationDataTable GetByDataObject(DestinationListFormData dataObject)
    {
      DestinationDataset.DestinationDataTable Destinations = new DestinationDataset.DestinationDataTable();
      if (dataObject == null)
      {
        return new DestinationDataset.DestinationDataTable();
      }

      Int32 MasterAreaToUse = masterAreaId;
      if (dataObject.ListAllMasterAreas == true)
        MasterAreaToUse = -1;

      Boolean HasDestinationType = (dataObject.DestinationTypeCode != null) && (dataObject.DestinationTypeCode != string.Empty);

      Boolean HasDestinationClassification = (dataObject.ClassificationCode != null) && (dataObject.ClassificationCode != string.Empty);

      Boolean HasDestinationCollection = (dataObject.CollectionCode != null) && (dataObject.CollectionCode != string.Empty);

      if ((HasDestinationCollection == true) && (HasDestinationType == true))
      {
        Destinations = GetByTypeCollection(MasterAreaToUse,dataObject.DestinationTypeCode, dataObject.CollectionCode);
      }

      if ((HasDestinationCollection == true) && (HasDestinationClassification == true))
      {
        Destinations = GetByClassificationCollection(MasterAreaToUse,dataObject.ClassificationCode, dataObject.CollectionCode);
      }

      if ((HasDestinationCollection == true) && (HasDestinationClassification == false) && (HasDestinationType == false))
      {
        Destinations = GetByCollection(MasterAreaToUse,dataObject.CollectionCode);
      }

      if ((HasDestinationCollection == false) && (HasDestinationType == true))
      {
        Destinations = GetByTypeCode(MasterAreaToUse,dataObject.DestinationTypeCode);
      }

      if ((HasDestinationCollection == false) && (HasDestinationClassification == true))
      {
        Destinations = GetByClassificationCode(MasterAreaToUse,dataObject.ClassificationCode);
      }

      if ((HasDestinationCollection == false) && (HasDestinationClassification == false) && (HasDestinationType == false))
      {
        Destinations = GetAll(MasterAreaToUse);
      }

      return Destinations;
    }

    public void Insert(DestinationDataset.DestinationRow Row)
    {
      ValidateRow(Row);

      destinationRepository.Insert(Row);
    }

    public static Boolean CanShowMore1(DestinationDataset.DestinationRow destination)
    {
      if (destination.ShortDescription != string.Empty)
        return true;
      if (destination.Comment1 != string.Empty)
        return true;
      if (destination.Image1Filename != string.Empty)
        return true;

      return false;
    }
    public static Boolean CanShowMore2(DestinationDataset.DestinationRow destination)
    {
      if (destination.LongDescription != string.Empty)
        return true;
      if (destination.Comment2 != string.Empty)
        return true;
      if (destination.Comment3 != string.Empty)
        return true;
      if (destination.Comment4 != string.Empty)
        return true;
      if (destination.Image2Filename != string.Empty)
        return true;

      return false;
    }
    public static Boolean CanShowMore3(DestinationDataset.DestinationRow destination)
    {
      if (destination.TelephoneNo != string.Empty)
        return true;
      if (destination.CellNo != string.Empty)
        return true;
      if (destination.Address != string.Empty)
        return true;
      if (destination.Booking != string.Empty)
        return true;
      if (destination.Language != string.Empty)
        return true;
      if (destination.Comment != string.Empty)
        return true;

      return false;
    }
    public static Boolean CanRouteTo(DestinationDataset.DestinationRow destination)
    {
      if ((destination.IsLatitudeNull() == true) ||
        (destination.IsLongitudeNull() == true) ||
        (destination.IsArrivalZoneTypeNull() == true) ||
        (destination.IsArrivalZoneDataNull() == true) ||
        (destination.IsArrivalZoneMinLatitudeNull() == true) ||
        (destination.IsArrivalZoneMaxLatitudeNull() == true) ||
        (destination.IsArrivalZoneMinLongitudeNull() == true) ||
        (destination.IsArrivalZoneMaxLongitudeNull() == true) ||
        (destination.IsGridReferenceXNull() == true) ||
        (destination.IsGridReferenceYNull() == true))
        return false;

      return true;     
    }

    public static DestinationDataset.DestinationRow CreateNew(float latitude, float longitude,string name)
    {
      DestinationDataset.DestinationDataTable RecentDestinations = new DestinationDataset.DestinationDataTable();
      DestinationDataset.DestinationRow Destination = RecentDestinations.NewDestinationRow();

      Destination.Code = name;
      Destination.Latitude = latitude;
      Destination.Longitude = longitude;
      Destination.ArrivalZoneData = CircularRegion.GetRegionData(Destination.Latitude, Destination.Longitude, DefaultArrivalZoneRadius);
      float MinLatitude;
      float MaxLatitude;
      float MinLongitude;
      float MaxLongitude;
      CircularRegion.CalculateMinMaxLatLong(Destination.Latitude, Destination.Longitude, DefaultArrivalZoneRadius, out MinLatitude, out MaxLatitude, out MinLongitude, out MaxLongitude);
      Destination.ArrivalZoneMinLatitude = MinLatitude;
      Destination.ArrivalZoneMaxLatitude = MaxLatitude;
      Destination.ArrivalZoneMinLongitude = MinLongitude;
      Destination.ArrivalZoneMaxLongitude = MaxLongitude;

      return Destination;
    }
    private void ValidateRow(DestinationDataset.DestinationRow Row)
    {
      logger.Write(this, ">> ValidateRow");
      if (Row.IsIdNull() == true)
        throw new DataException("Destination.Id is null");

      if (Row.IsMasterAreaIdNull() == true)
        throw new DataException("Destination.MasterArea is null");

//      if (Row.IsThemeIdNull() == true)
//        throw new DataException("Destination.Theme is null");

      if (Row.IsDestinationTypeIdNull() == true)
        throw new DataException("Destination.DestinationType is null");

      if (Row.IsClassificationIdNull() == true)
        throw new DataException("Destination.ClassificationId is null");
/*
      if ((Row.IsLatitudeNull() == true) &&
        (Row.IsLongitudeNull() == true) &&
        (Row.IsArrivalZoneTypeNull() == true) &&
        (Row.IsArrivalZoneDataNull() == true) &&
        (Row.IsArrivalZoneMinLatitudeNull() == true) &&
        (Row.IsArrivalZoneMaxLatitudeNull() == true) &&
        (Row.IsArrivalZoneMinLongitudeNull() == true) &&
        (Row.IsArrivalZoneMaxLongitudeNull() == true) &&
        (Row.IsGridReferenceXNull() == true) &&
        (Row.IsGridReferenceYNull() == true))
        throw new DataException("Incomplete geographical data");
*/
      if (Row.IsRecommendationNull() == true)
        Row.Recommendation = 0;

      if (Row.IsVersionNoNull() == true)
        Row.VersionNo = 0;

      if (Row.IsIncludeInNearestSearchNull() == true)
        Row.IncludeInNearestSearch = false;

      Row.SearchKey = Row.Code.ToUpper().Substring(0,SearchKeyWidth);

      logger.Write(this, "<< ValidateRow");
    }

    private readonly ILogger logger;
    private readonly IDestinationRepository destinationRepository = null;
    private readonly IDestinationTypeRepository destinationTypeRepository = null;
    private readonly IDestinationClassificationRepository destinationClassificationRepository = null;
    private readonly IDestinationCollectionRepository destinationCollectionRepository = null;
    private readonly ItineraryDayBll itineraryDayBll = null;
    private Int32 masterAreaId = -1;
    private readonly IDestinationTypeImageIndexMapper imageMapper = null;
}

}
