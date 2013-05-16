using System;
using System.Collections.Generic;
using Nucleo.Data;
using Nucleo.Math.Geo;

namespace Nucleo.GoodGuide.Types.DataObjects
{
  public class Destination
  {
    public ValidationResult Validate()
    {
      ValidationResult result = new ValidationResult();

      if (Id == null)
        result.AddValidationError("Id is undefined");

      return result;
    }

    #region Fields
    // Fields are public for performance on CF2

    public Int32? Id;
		public string Code;
		public string ShortDescription;
		public string LongDescription;
		public Int32? DestinationTypeId;
		public Int32? ClassificationId;
		public string ArrivalZoneData;
		public string Address;
		public string TelephoneNo;
		public Int32? MasterAreaId;
		public Int32? ThemeId;
		public Int32? VersionNo;
		public Int16? Recommendation;
		public string Location;
		public string Condition;
		public decimal? Latitude;
		public decimal? Longitude;
		public string CellNo;
		public string Image1Filename;
		public string Image2Filename;
		public string Comment1;
		public string Comment2;
		public string Comment3;
		public string Comment4;
		public string Booking;
		public string Language;
		public string Comment;
		public Int32? CollectionId;
		public string ArrivalZoneType;
		public decimal? ArrivalZoneMinLatitude;
		public decimal? ArrivalZoneMaxLatitude;
		public decimal? ArrivalZoneMinLongitude;
		public decimal? ArrivalZoneMaxLongitude;
		public Int32? GridReferenceX;
		public Int32? GridReferenceY;
		public bool IncludeInNearestSearch;
    public string SearchKey;
    
    public Int16? TypeImageIndex;
    public string RecommendationText;
    public Int32? Distance;
    public string DistanceText;
    public Int16? Direction;
    public Int16? DirectionImageIndex;
    public string DirectionText;
    public string DestinationTypeDescription;

    #endregion
  }

  public class Destinations : List<Destination>
  {
    public void SortByDistance(double latitude, double longitude)
    {
      this.Sort(new DestinationDistanceComparer(latitude, longitude));
    }

  }

  internal class DestinationDistanceComparer :
    IComparer<Destination>
  {
    public DestinationDistanceComparer(double latitude, double longitude)
    {
      this.latitude = latitude;
      this.longitude = longitude;
    }

    #region IComparer<Street>
    public int Compare(Destination x, Destination y)
    {
      Guard.ArgumentNotNull(x, "x");
      Guard.ArgumentNotNull(y, "y");

      double xDistance = GeoCalculations.ApproximateDistance(latitude, longitude, (double)x.Latitude.Value, (double)x.Longitude.Value);
      double yDistance = GeoCalculations.ApproximateDistance(latitude, longitude, (double)y.Latitude.Value, (double)y.Longitude.Value);

      if (xDistance == yDistance)
        return 0;
      if (yDistance > xDistance)
        return -1;

      return 1;
    }
    #endregion

    private readonly double latitude;
    private readonly double longitude;
  }

}
