using System;
using Telogis.GeoBase.Routing;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  public static class RoutingStrategyFactory
  {
    public static Telogis.GeoBase.Routing.RoutingStrategy Create(string strategy)
    {
      Guard.ArgumentNotNullOrEmptyString(strategy, "strategy");

      switch(strategy.ToUpper()[0])
      {
        case 'F': return new RoutingStrategyFastest();
        case 'S': return new RoutingStrategyShortest();
      }

      throw new InvalidOperationException(string.Format("Unhandled case for strategy {0}",strategy));
    }
  }
}
