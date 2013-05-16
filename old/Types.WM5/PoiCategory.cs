using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types
{
  public class PoiCategory
  {
    public PoiCategory()
    {
    }
    public PoiCategory(int index,string name)
    {
      this.index = index;
      this.name = name;
    }

    public int Index
    {
      get { return index; }
      set { index = value; }
    }

    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    private Int32 index;
    private string name = string.Empty;
  }

  public class PoiCategoryComparer : IComparer<PoiCategory>
  {
    public Int32 Compare(PoiCategory poiCategory1, PoiCategory poiCategory2)
    {
      return poiCategory1.Name.CompareTo(poiCategory2.Name);
    }
  }

  public class PoiCategories : List<PoiCategory>
  {
    public PoiCategory GetByName(string name)
    {
      for (Int32 CategoryNo = 0; CategoryNo < this.Count; CategoryNo++)
        if (this[CategoryNo].Name == name)
          return this[CategoryNo];

      return null;
    }
  }
}
