using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class MultilineTextButton : Control
  {
    public const string LineSeparator = "|";

    public MultilineTextButton()
    {
      this.Width = 150;
      this.Height = 20;

      this.BackColor = SystemColors.Window;
      this.ForeColor = SystemColors.WindowText;

      format.Alignment = StringAlignment.Center;
      format.LineAlignment = StringAlignment.Center;

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
    public new Boolean Enabled
    {
      get
      {
        return base.Enabled;
      }
      set
      {
        base.Enabled = value;
        Invalidate();
      }
    }
    public Int16 LineSeparation
    {
      get { return lineSeparation; }
      set { lineSeparation = value; }
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
    public Int16 Margin
    {
      get { return margin; }
      set
      {
        margin = value;
        Invalidate();
      }
    }
    public ContentAlignment TextAlign
    {
      get { return alignment; }
      set
      {
        alignment = value;
        switch (alignment)
        {
          case ContentAlignment.TopCenter:
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;
            break;
          case ContentAlignment.TopLeft:
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Near;
            break;
          case ContentAlignment.TopRight:
            format.Alignment = StringAlignment.Far;
            format.LineAlignment = StringAlignment.Far;
            break;
        }

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
        Rectangle SourceRect = new Rectangle(0, 0, ImageWidth, ImageHeight);
        g.DrawImage(BackgroundImage, 0, 0, SourceRect, GraphicsUnit.Pixel);
      }

      if (Text.Trim() == string.Empty)
        return;

      brush.Color = this.ForeColor;

      // Get lines
      string[] Lines = Text.Split(LineSeparator.ToCharArray());

      // Get total height
      Int32 TotalHeight = 0;

      for (Int16 LineNo = 0; LineNo < Lines.Length;LineNo++ )
      {
        SizeF TextSize = g.MeasureString(Lines[LineNo], Font);
        TotalHeight += (Int16)TextSize.Height;
      }

      // Draw lines
      Int16 TextTop = (Int16)((this.Height - TotalHeight - (2 * margin)) / 2 + (lineSeparation * (Lines.Length - 1)) + margin);
      for (Int16 LineNo = 0; LineNo < Lines.Length; LineNo++)
      {
        SizeF TextSize = g.MeasureString(Lines[LineNo], Font);
        RectangleF StringBounds = new RectangleF(margin, TextTop, Width - (2 * margin), TextSize.Height);
        g.DrawString(Lines[LineNo], this.Font, brush, StringBounds,format);

        TextTop += (Int16)(TextSize.Height + lineSeparation);
      }


    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private Int16 lineSeparation = 1;
    private Int16 margin = 1;
    private Image activeBackgroundImage = null;
    private Image inactiveBackgroundImage = null;
    private ContentAlignment alignment = ContentAlignment.TopCenter;
    private readonly StringFormat format = new StringFormat();

  }
}