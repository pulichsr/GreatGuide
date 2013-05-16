using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public interface IListItemField
  {
    Int16 Left { get; }
    Int16 Top { get; }
    Int16 Width { get; }
    Int16 Height { get; }
    string FieldName { get; }

    void Draw(Control parent,Graphics g,Pen pen,SolidBrush brush,object dataSource);
  }

  public class ListItemField
  {
    public Int16 Left
    {
      get { return left; }
      set { left = value; }
    }
    public Int16 Top
    {
      get { return top; }
      set { top = value; }
    }
    public Int16 Width
    {
      get { return width; }
      set { width = value; }
    }
    public Int16 Height
    {
      get { return height; }
      set { height = value; }
    }
    public string FieldName
    {
      get { return fieldName; }
      set { fieldName = value; }
    }

    protected object GetFieldValue(object dataSource)
    {
      if (fieldName == string.Empty)
        return null;

      PropertyInfo PropertyInfo = dataSource.GetType().GetProperty(fieldName);
      if (PropertyInfo == null)
        return null;

      return PropertyInfo.GetValue(dataSource,null);
    }
    
    private Int16 left = 0;
    private Int16 top = 0;
    private Int16 width = 10;
    private Int16 height = 10;
    private string fieldName;
  }
}
