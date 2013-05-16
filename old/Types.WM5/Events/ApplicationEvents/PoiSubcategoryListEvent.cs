using System;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class PoiSubcategoryListEvent : ApplicationEvent
  {
    public PoiSubcategoryListEvent()
    {
    }
    public PoiSubcategoryListEvent(int categoryIndex)
    {
      this.categoryIndex = categoryIndex;
    }
    public PoiSubcategoryListEvent(PoiSubcategories subcategories)
    {
      this.subcategories = subcategories;
    }

    public Int32 CategoryIndex
    {
      get { return categoryIndex; }
      set { categoryIndex = value; }
    }
    public PoiSubcategories Subcategories
    {
      get { return subcategories; }
      set { subcategories = value; }
    }

    private Int32 categoryIndex;
    private PoiSubcategories subcategories = null;
  }
}
