namespace Nucleo.GoodGuide.Types
{
  public static class NotificationDefinitionsFactory
  {
    public const char DefinitionSeparator = ';';

    public static NotificationDefinitions Create(string metadata)
    {
      Guard.ArgumentNotNullOrEmptyString(metadata, "metadata");

      NotificationDefinitions definitions = new NotificationDefinitions();

      string[] fields = metadata.Split(DefinitionSeparator);
      if (fields.Length == 0)
        return definitions;

      foreach (string field in fields)
      {
        definitions.Add(NotificationDefinitionFactory.Create(field));
      }

      return definitions;
    }
  }
}