using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using Nucleo.Xml;
using Nucleo.Windows.Forms;

namespace Nucleo.GoodGuide.CarApplication
{
  public class TemplateManager : ResourceAssembly
  {
    public TemplateManager(Assembly assembly)
      : base(assembly)
    {
    }

    public XmlTextReader GetXml(string resourceName)
    {

      if (cache.ContainsKey(resourceName) == false)
      {
        Stream resourceStream = Get(resourceName + ".xml");
        if (resourceStream == null)
          return null;

        TextReader textReader = new StreamReader(resourceStream);
        cache[resourceName] = textReader.ReadToEnd();
      }

      MemoryStream xmlStream = new MemoryStream(Encoding.ASCII.GetBytes(cache[resourceName]));
      return new XmlTextReader(xmlStream);
    }
    public string GetString(string resourceName)
    {

      if (cache.ContainsKey(resourceName) == false)
      {
        Stream resourceStream = Get(resourceName);
        if (resourceStream == null)
          return null;

        TextReader textReader = new StreamReader(resourceStream);
        cache[resourceName] = textReader.ReadToEnd();
      }

      return cache[resourceName];
    }

    public KeyboardKeyDefinitions GetKeyDefinitions(string resourceName)
    {
      if (keyboardKeyDefinitions.ContainsKey(resourceName) == false)
      {
        string xml = GetString(resourceName);
        keyboardKeyDefinitions[resourceName] = XmlSerialization.DeserializeFromString<KeyboardKeyDefinitions>(xml);
      }

      return keyboardKeyDefinitions[resourceName];
    }

    private readonly Dictionary<string,string> cache = new Dictionary<string,string>();
    private readonly Dictionary<string,KeyboardKeyDefinitions> keyboardKeyDefinitions = new Dictionary<string,KeyboardKeyDefinitions>();
  }
}