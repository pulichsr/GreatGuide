using System;
using System.Collections.Generic;

namespace Nucleo.GoodGuide.Types
{
  public class PoiSubcategory
  {
    public const Int32 AllSubcategories = -1;

    public PoiSubcategory()
    {
    }
    public PoiSubcategory(Int32 index, Int32 categoryIndex,string name)
    {
      this.index = index;
      this.categoryIndex = categoryIndex;
      this.name = name;
    }

    public int Index
    {
      get { return index; }
      set { index = value; }
    }
    public int CategoryIndex
    {
      get { return categoryIndex; }
      set { categoryIndex = value; }
    }
    public string Name
    {
      get { return name; }
      set { name = value; }
    }

    private Int32 index;
    private Int32 categoryIndex;
    private string name = string.Empty;
  }

  public class PoiSubcategoryComparer : IComparer<PoiSubcategory>
  {
    public Int32 Compare(PoiSubcategory poiSubcategory1, PoiSubcategory poiSubcategory2)
    {
      return poiSubcategory1.Name.CompareTo(poiSubcategory2.Name);
    }
  }

  public class PoiSubcategories : List<PoiSubcategory>
  {}
}
