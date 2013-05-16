using System;

namespace Nucleo.GoodGuide.Types.Configuration
{
  [Serializable]
  public class ContentManagerConfiguration
  {
    public string ContentBasePath
    {
      get { return contentBasePath; }
      set { contentBasePath = value; }
    }

    private string contentBasePath;
  }
}