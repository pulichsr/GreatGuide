using System.Drawing;
using System.IO;

namespace Nucleo.GoodGuide.CarApplicationResources
{
  public static class ImageResources
  {
    private static readonly string resourcePathRoot = "Nucleo.GoodGuide.CarApplicationResources.Images.";

    public static void IterateResources()
    {
      foreach (string resource in System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames())
      {
        using (StreamWriter streamWriter = new StreamWriter("IterateResources.txt", true))
        {
          streamWriter.WriteLine(resource);
        }
      }
    }

    public static class frmDisclaimer
    {
      public static Bitmap Background
      {
        get
        {
          return resourceManager.GetBitmap(string.Format("{0}Background.bmp",resourcePath));
        }
      }
      public static Bitmap btnAccept
      {
        get
        {
          return resourceManager.GetBitmap(string.Format("{0}btnAccept.bmp", resourcePath));
        }
      }

      private static readonly string resourcePath = resourcePathRoot + "frmDisclaimer.";
    }

    public static class frmWelcome
    {
      public static Bitmap Background
      {
        get
        {
          return resourceManager.GetBitmap(string.Format("{0}Background.bmp",resourcePath));
        }
      }
      public static Bitmap btnSetup
      {
        get
        {
          return resourceManager.GetBitmap(string.Format("{0}btnSetup.bmp", resourcePath));
        }
      }
      public static Bitmap btnContinue
      {
        get
        {
          return resourceManager.GetBitmap(string.Format("{0}btnContinue.bmp", resourcePath));
        }
      }

      private static readonly string resourcePath = resourcePathRoot + "frmWelcome.";
    }

    private readonly static BitmapResourceManager resourceManager = new BitmapResourceManager(System.Reflection.Assembly.GetExecutingAssembly());
  }
}

