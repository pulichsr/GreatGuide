using System.Drawing;
using System.IO;
using Nucleo.GoodGuide.Types;
using Nucleo.GoodGuide.Types.Interfaces;

namespace Nucleo.GoodGuide.CarApplication
{
  public class WelcomeAdProvider:
    IWelcomeAdProvider
  {
    public WelcomeAdProvider(ILogger logger,Language language)
    {
      this.logger = logger;
      this.language = language;

    }
    public Bitmap GetAd()
    {
      string adFilename = LanguageHelper.FormatCultureDependentPath(
        LanguageHelper.CreateCulture(language),
        string.Format("{0}\\Resources\\",Nucleo.Path.ExecutablePath),
        "WelcomeAd.bmp");
      if (File.Exists(adFilename) == false)
      {
        logger.Write(this,string.Format("Ad {0} not found",adFilename));
        return null;
      }

      logger.Write(this, string.Format("Loading ad file {0}", adFilename));

      return new Bitmap(adFilename);
    }

    private readonly ILogger logger;
    private readonly Language language;
    private readonly string filename;
  }
}
