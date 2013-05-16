using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class KeyboardButton : Control
  {
    public KeyboardButton()
    {
      this.Width = 150;
      this.Height = 20;

      this.BackColor = SystemColors.Window;
      this.ForeColor = SystemColors.WindowText;

      CreateGDIObjects();
    }

    public new string Text
    {
      get { return base.Text; }
      set
      {
        base.Text = value;
        Invalidate();
      }
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

      SizeF TextSize = g.MeasureString(Text, Font);

      RectangleF StringBounds = new RectangleF((Width - TextSize.Width) / 2, (Height - TextSize.Height) / 2, TextSize.Width, TextSize.Height);

      brush.Color = this.ForeColor;
      g.DrawString(Text, this.Font, brush, StringBounds);
    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
  }
}