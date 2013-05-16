using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class ListItemTextField : ListItemField, IListItemField
  {
    public ListItemTextField()
    {
    }
    public ListItemTextField(Font font,string text)
    {
      this.font = font;
      this.text = text;
    }

    public Font Font
    {
      get { return font; }
      set { font = value; }
    }
    public string Text
    {
      get { return text; }
      set { text = value; }
    }
    public Color BackColor
    {
      get { return backColor; }
      set { backColor = value; }
    }
    public Color ForeColor
    {
      get { return foreColor; }
      set { foreColor = value; }
    }

    public void Draw(Control parent, Graphics g, Pen pen, SolidBrush brush, object dataSource)
    {
      string TextToDraw = Text;

      try
      {
        if (dataSource != null)
        {
          object Value = GetFieldValue(dataSource);
          if (Value != null)
            TextToDraw = Value.ToString();
        }       
      }
      catch
      {
        return;
      }

      if (TextToDraw == string.Empty)
        return;

      brush.Color = foreColor;
      RectangleF StringBounds = new RectangleF(Left, Top, Width, Height);

      if (font == null)
        g.DrawString(TextToDraw, parent.Font, brush, StringBounds);      
      else
        g.DrawString(TextToDraw, this.Font, brush, StringBounds);      
    }

    private Font font = null;
    private string text;
    private Color backColor;
    private Color foreColor;
  }
}
