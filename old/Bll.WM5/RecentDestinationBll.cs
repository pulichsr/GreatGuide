using System;
using System.Data;
using Nucleo.GoodGuide.Datasets.Datasets;
using Nucleo.GoodGuide.Types.Interfaces.Dal;
using Nucleo.GoodGuide.Types;

namespace Nucleo.GoodGuide.Bll
{
  public class RecentDestinationBll
  {
    public const Int32 UndefinedId = -1;

    public RecentDestinationBll(IRecentDestinationRepository repository,IDestinationTypeImageIndexMapper imageMapper)
    {
      this.repository = repository;
      this.imageMapper = imageMapper;
    }

    public void Insert(RecentDestinationDataset.RecentDestinationRow Row)
    {
      ValidateRow(Row);

      repository.Insert(Row);
    }
    public void DeleteAll()
    {
      repository.DeleteAll();
    }
    public RecentDestinationDataset.RecentDestinationDataTable GetAll()
    {
      RecentDestinationDataset.RecentDestinationDataTable Destinations = repository.Get();
      AddDisplayColumns(Destinations);

      foreach (RecentDestinationDataset.RecentDestinationRow row in Destinations.Rows)
      {
        
      }

      return Destinations;
    }

    public void AddDisplayColumns(RecentDestinationDataset.RecentDestinationDataTable destinations)
    {
      if (destinations == null)
        return;

      destinations.Columns.Add("TypeImageIndex", typeof(Int16));
      destinations.Columns.Add("Distance", typeof(Int32));
      destinations.Columns.Add("DistanceText", typeof(string));
      destinations.Columns.Add("Direction", typeof(Int16));
      destinations.Columns.Add("DirectionImageIndex", typeof(Int16));
      destinations.Columns.Add("DirectionText", typeof(string));

      SetTypeImageIndex(destinations);
    }
    public void CalculateDistanceDirection(RecentDestinationDataset.RecentDestinationDataTable destinations, float latitude, float longitude,Units units)
    {
      if (destinations == null)
        return;

      if (destinations.Columns["Distance"] == null)
        return;

      for (Int32 DestinationNo = 0; DestinationNo < destinations.Rows.Count; DestinationNo++)
      {
        RecentDestinationDataset.RecentDestinationRow Destination = destinations[DestinationNo];
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
    public void Sort(RecentDestinationDataset.RecentDestinationDataTable destinations, string sortKey)
    {
      if (destinations == null)
        return;
      if (destinations.Count == 0)
        return;

      destinations.DefaultView.Sort = sortKey;
    }
    public void SetTypeImageIndex(RecentDestinationDataset.RecentDestinationDataTable destinations)
    {
      if (destinations == null)
        return;

      if (destinations.Columns["TypeImageIndex"] == null)
        return;

//      for (Int32 DestinationNo = 0; DestinationNo < destinations.Rows.Count; DestinationNo++)
//      {
//        RecentDestinationDataset.RecentDestinationRow Destination = destinations[DestinationNo];
//        if (Destination.IsDestinationTypeIdNull() == true)
//          continue;

//        Destination["TypeImageIndex"] = imageMapper.DestinationTypeToImageIndex(Destination.DestinationTypeId);
//      }
    }

    private static void ValidateRow(RecentDestinationDataset.RecentDestinationRow Row)
    {
      if (Row.IsVisitedDatNull() == true)
        throw new DataException("RecentDestination.VisitedDat is null");

      if (Row.IsLatitudeNull() == true)
        throw new DataException("RecentDestination.Latitude is null");

      if (Row.IsLongitudeNull() == true)
        throw new DataException("RecentDestination.Longitude is null");
    }

    private readonly IRecentDestinationRepository repository = null;
    private readonly IDestinationTypeImageIndexMapper imageMapper = null;
  }

}
