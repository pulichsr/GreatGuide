using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class TwoLineMenuButton : Control
  {
    public TwoLineMenuButton()
    {
      this.Width = 150;
      this.Height = 20;

      this.BackColor = SystemColors.Window;
      this.ForeColor = SystemColors.WindowText;

      CreateGDIObjects();
    }

    public override string Text
    {
      get
      {
        return base.Text;
      }
      set
      {
        base.Text = value;
        Invalidate();
      }
    }
    public string DescriptionText
    {
      get { return descriptionText; }
      set
      {
        descriptionText = value;
        Invalidate();
      }
    }
    public Image ActiveBackgroundImage
    {
      set
      {
        activeBackgroundImage = value;
        Invalidate();
      }
    }
    public Image InactiveBackgroundImage
    {
      set
      {
        inactiveBackgroundImage = value;
        Invalidate();
      }
    }
    public Font DescriptionFont
    {
      get { return descriptionFont; }
      set { descriptionFont = value; }
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

      Image BackgroundImage;
      if (Enabled == true)
        BackgroundImage = activeBackgroundImage;
      else
        BackgroundImage = inactiveBackgroundImage;

      if (BackgroundImage != null)
      {
        Int32 ImageWidth = BackgroundImage.Width;
        if (ImageWidth > Width)
          ImageWidth = Width;
        Int32 ImageHeight = BackgroundImage.Height;
        if (ImageHeight > Height)
          ImageHeight = Height;
        Rectangle SourceRect = new Rectangle(0, 0, ImageWidth,ImageHeight);
        g.DrawImage(BackgroundImage, 0, 0, SourceRect, GraphicsUnit.Pixel);
      }

      // Draw text
      brush.Color = this.ForeColor;
      SizeF TextSize = g.MeasureString(Text, Font);
      RectangleF StringBounds = new RectangleF((Width - TextSize.Width) / 2, 0, TextSize.Width, TextSize.Height);
      g.DrawString(Text, this.Font, brush, StringBounds);

      // Draw description text
      TextSize = g.MeasureString(descriptionText, descriptionFont);
      StringBounds = new RectangleF((Width - TextSize.Width) / 2, Height - TextSize.Height - 6, TextSize.Width, TextSize.Height);
      g.DrawString(descriptionText, descriptionFont, brush, StringBounds);
    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private string descriptionText = string.Empty;
    private Image activeBackgroundImage = null;
    private Image inactiveBackgroundImage = null;
    private Font descriptionFont = new Font("Arial",8,FontStyle.Regular);
  }
}