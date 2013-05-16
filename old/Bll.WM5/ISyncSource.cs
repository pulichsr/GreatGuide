using System.Collections.Generic;
using System.Data;

namespace Nucleo.GoodGuide.Bll
{
  public interface ISyncSource
  {
    string DatasetName
    {
      get;
    }

    DataTable Get();
  }

  public class SyncSources : List<ISyncSource>
  {
  }

}
