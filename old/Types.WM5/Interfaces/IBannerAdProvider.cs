using System;
using System.Drawing;

namespace Nucleo.GoodGuide.Types.Interfaces
{
  public interface IBannerAdProvider
  {
    Bitmap GetAd(Int32 masterAreaId, string formName);
  }
}
