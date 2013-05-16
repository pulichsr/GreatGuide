using System;
using System.Text;
using Telogis.GeoBase;
using Telogis.GeoBase.Navigation;

namespace Nucleo.GoodGuide.GeoBaseNavigator
{
  public class ObservableAnnouncer: XmlWaveAnnouncer
  {
    public event EventHandler<TextEventArgs> SayingMovement;

    protected override void SayMovement(PlayItem play, NavigationEvent mvmt, bool playDist, NavigationEvent mvmt2, bool playDist2)
    {
      base.SayMovement(play, mvmt, playDist, mvmt2, playDist2);

      if (SayingMovement == null)
        return;

      StringBuilder message = new StringBuilder();
      message.Append("SayMovement\r\n");
      message.Append(string.Format("  DirectionQualifier:{0}\r\n", mvmt.DirectionQualifier));
      message.Append(string.Format("  DirectionType:{0}\r\n", mvmt.DirectionType));
      message.Append(string.Format("  GetDistance:{0}\r\n", mvmt.GetDistance(DistanceUnit.METERS)));
      message.Append(string.Format("  Number:{0}\r\n", mvmt.Number));
      message.Append(string.Format("  TargetStreet:{0}\r\n", mvmt.TargetStreet));
      message.Append(string.Format("  TurnDirection:{0}\r\n", mvmt.TurnDirection));

      SayingMovement(this,new TextEventArgs(message.ToString()));
    }
  }
}
