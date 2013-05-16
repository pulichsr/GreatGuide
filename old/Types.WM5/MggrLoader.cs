using System;
using System.IO;
using Nucleo.GoodGuide.Datasets.Datasets;

namespace Nucleo.GoodGuide.Types
{
  public class MggrLoader
  {
    public GpsRegionDataset.GpsRegionDataTable Load(string filename)
    {
      GpsRegionDataset Dataset = new GpsRegionDataset();
      GpsRegionDataset.GpsRegionRow Row = null;

      if (File.Exists(filename) == false)
        throw new FileNotFoundException(string.Format("{0} does not exist",filename));

      TextReader MggrReader = new StreamReader(filename);

      try
      {
        while (true)
        {
          string Line = MggrReader.ReadLine();
          if (Line == null)
            break;

          string[] MggrFields = Line.Split(',');
          if (MggrFields.Length < 9)
            continue;

          Row = Dataset.GpsRegion.NewGpsRegionRow();
          Row.Id = Convert.ToInt32(MggrFields[0]);
          Row.MinLatitude = Convert.ToSingle(MggrFields[3]);
          Row.MaxLatitude = Convert.ToSingle(MggrFields[4]);
          Row.MinLongitude = Convert.ToSingle(MggrFields[5]);
          Row.MaxLongitude = Convert.ToSingle(MggrFields[6]);
          Row.RegionType = "C";
          Row.ResetOnEntry = false;

          float Latitude = Convert.ToSingle(MggrFields[1]);
          float Longitude = Convert.ToSingle(MggrFields[2]);
          Int32 Radius = Convert.ToInt32(MggrFields[7]);
          Int32 RadiusSquared = Convert.ToInt32(MggrFields[8]);
          Row.RegionData = string.Format("{0},{1}|{2}|{3}",Latitude,Longitude,Radius,RadiusSquared);

          Dataset.GpsRegion.AddGpsRegionRow(Row);
        }
      }
      finally
      {
        MggrReader.Close();
      }

      return Dataset.GpsRegion;
    }
  }
}
