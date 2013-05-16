using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class DestinationCommentLine : Control
  {
    public DestinationCommentLine()
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
    public string CommentText
    {
      get { return commentText; }
      set
      {
        commentText = value;
        Invalidate();
      }
    }
    public Image BackgroundImage
    {
      set
      {
        backgroundImage = value;
        Invalidate();
      }
    }
    public Font CommentFont
    {
      get { return commentFont; }
      set { commentFont = value; }
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

      if (backgroundImage != null)
      {
        Int32 ImageWidth = backgroundImage.Width;
        if (ImageWidth > Width)
          ImageWidth = Width;
        Int32 ImageHeight = backgroundImage.Height;
        if (ImageHeight > Height)
          ImageHeight = Height;
        Rectangle SourceRect = new Rectangle(0, 0, ImageWidth,ImageHeight);
        g.DrawImage(backgroundImage, 0, 0, SourceRect, GraphicsUnit.Pixel);
      }

      // Draw text
      brush.Color = this.ForeColor;
      SizeF TextSize = g.MeasureString(Text, Font);
      RectangleF StringBounds = new RectangleF(2, (Height - TextSize.Height) / 2, TextSize.Width, TextSize.Height);
      g.DrawString(Text, this.Font, brush, StringBounds);

      // Draw comment text
      SizeF CommentTextSize = g.MeasureString(commentText, commentFont);
      StringBounds = new RectangleF(TextSize.Width + 5, (Height - TextSize.Height) / 2, CommentTextSize.Width, CommentTextSize.Height);
      g.DrawString(commentText, commentFont, brush, StringBounds);
    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private string commentText = string.Empty;
    private Image backgroundImage = null;
    private Font commentFont = new Font("Arial",8,FontStyle.Regular);
  }
}