using System.Windows.Forms;

namespace Nucleo.GoodGuide.Types.Events.ApplicationEvents
{
  public class GetTextEvent : ApplicationEvent
  {
    public DialogResult DialogResult
    {
      get { return dialogResult; }
      set { dialogResult = value; }
    }
    public GetTextEvent(string prompt)
    {
      this.prompt = prompt;
    }

    public string Text
    {
      get { return text; }
      set { text = value; }
    }
    public string Prompt
    {
      get { return prompt; }
      set { prompt = value; }
    }

    private DialogResult dialogResult = DialogResult.None;
    private string text = string.Empty;
    private string prompt = string.Empty;
  }
}
