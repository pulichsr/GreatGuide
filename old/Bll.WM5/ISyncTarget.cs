using System;
using System.Collections.Generic;
using Nucleo.GoodGuide.Datasets.XmlDatasets;

namespace Nucleo.GoodGuide.Bll
{
  public interface ISyncTarget
  {
    string DatasetName
    {
      get;
    }

    void Insert(GetDataResponse newData,Int32 offset,Boolean merge);
    void DeleteAll();
  }

  public class SyncTargets : List<ISyncTarget>
  {
  }

}


