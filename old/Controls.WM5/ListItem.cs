using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class ListItem : Control
  {
//    public delegate Boolean Predicate<btnEight>(btnEight obj);

    public ListItem()
    {
      this.Width = 150;
      this.Height = 20;

      this.BackColor = SystemColors.Window;
      this.ForeColor = SystemColors.WindowText;

      CreateGDIObjects();
    }
    protected override void Dispose(bool disposing)
    {
      if (bitmap != null)
        bitmap.Dispose();

      if (pen != null)
        pen.Dispose();

      if (brush != null)
        brush.Dispose();

      base.Dispose(disposing);
    }

    public ListItemTemplate Template
    {
      set { template = value; }
    }
    public object DataSource
    {
      set { dataSource = value; }
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
      // don't pass to base since we paint everything, avoid flashing
    }
    protected override void OnPaint(PaintEventArgs e)
    {
      // Draw on memory bitmap
      CreateMemoryBitmap();

      // Draw
      Graphics g = Graphics.FromImage(bitmap);
      Draw(g);

      // Blit memory bitmap to screen
      e.Graphics.DrawImage(bitmap, 0, 0);
    }
    protected override void OnResize(EventArgs e)
    {
      Invalidate();
    }

    private void CreateGDIObjects()
    {
      brush = new SolidBrush(this.ForeColor);
      pen = new Pen(this.ForeColor);
    }
    private void CreateMemoryBitmap()
    {
      // only create if don't have one of size changed
      if (bitmap == null || bitmap.Width != this.Width || bitmap.Height != this.Height)
      {
        // memory bitmap to draw on
        bitmap = new Bitmap(this.Width, this.Height);
      }
    }
    private void Draw(Graphics g)
    {
      // Background
      g.Clear(this.BackColor);

      if (template == null)
        return;

      pen.Color = ForeColor;
      g.DrawRectangle(pen,Left,Top,Width,Height);

      for (Int32 FieldNo = 0;FieldNo < template.Count;FieldNo++)
        template[FieldNo].Draw(this,g,pen,brush,dataSource);
    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private ListItemTemplate template = null;
    private object dataSource = null;
  }

  public class ListItems : List<ListItem>
  {
    public ListItemTemplate Template
    {
      get { return template; }
      set
      {
        template = value;

        for (Int32 ItemNo = 0; ItemNo < Count; ItemNo++)
          this[ItemNo].Template = template;
      }
    }
    public object DataSource
    {
      get { return dataSource; }
      set
      {
        dataSource = value;

        for (Int32 ItemNo = 0; ItemNo < Count; ItemNo++)
          this[ItemNo].DataSource = dataSource;
      }
    }

    public new void Add(ListItem item)
    {
      item.Template = template;
      item.DataSource = dataSource;

      base.Add(item);
    }

    private ListItemTemplate template = null;
    private object dataSource = null;
  }
}