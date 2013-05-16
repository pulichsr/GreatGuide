
namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class EntityEvent : ApplicationEvent
  {
    public EntityEvent()
    {
    }
    public EntityEvent(object entity)
    {
      this.entity = entity;
    }

    public object Entity
    {
      get { return entity; }
    }

    private object entity = null;

  }
}
