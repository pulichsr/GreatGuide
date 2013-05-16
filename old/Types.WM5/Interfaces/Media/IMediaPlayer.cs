using System;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Events;
using Nucleo.GoodGuide.Types.Events.ApplicationEvents;

namespace Nucleo.GoodGuide.Types.Interfaces.Media
{
  public interface IMediaPlayer
  {
    event EventHandler<GoodGuideEventArgs> MediaStateChanged;
    event EventHandler<GoodGuideEventArgs> MediaPositionChanged;

    void Initialise();
    void Finalise();

    string ContentBasePath {set;}

    MediaControlEvent.MediaControlResults Play(string filename,Boolean filenameIncludesPath,MediaTypes mediaTypeToPlay);
    MediaControlEvent.MediaControlResults Stop();
    MediaControlEvent.MediaControlResults Pause();
    MediaControlEvent.MediaControlResults Resume();
    void SetPosition(UInt16 position); // percentage
    void ToggleHalfFull();
    void SetMediaType(MediaTypes mediaType);
  }
}