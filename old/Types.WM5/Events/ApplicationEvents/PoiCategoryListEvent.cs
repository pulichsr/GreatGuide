namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class PoiCategoryListEvent : ApplicationEvent
  {
    public PoiCategoryListEvent()
    {
    }
    public PoiCategoryListEvent(PoiCategories categories)
    {
      this.categories = categories;
    }

    public PoiCategories Categories
    {
      get { return categories; }
      set { categories = value; }
    }

    private PoiCategories categories = null;
  }
}
