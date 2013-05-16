using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class More1Panel : Control
  {
    public More1Panel()
    {
      this.Width = 480;
      this.Height = 180;

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
    public Image Image
    {
      set
      {
        image = value;
        Invalidate();
      }
    }
    public Image CommentBackgroundImage
    {
      set
      {
        commentBackgroundImage = value;
        Invalidate();
      }
    }
    public string Comment1Label
    {
      get { return comment1Label; }
      set
      {
        comment1Label = value;
        Invalidate();
      }
    }
    public string Comment1
    {
      get { return comment1; }
      set
      {
        comment1 = value;
        Invalidate();
      }
    }
    public Font CommentFont
    {
      get { return commentFont; }
      set { commentFont = value; }
    }
    public Color CommentForeColor
    {
      get { return commentForeColor; }
      set
      {
        commentForeColor = value;
        Invalidate();
      }
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

      brush.Color = this.ForeColor;
      RectangleF StringBounds = new RectangleF(4, 0, 214, 155);
      g.DrawString(Text, this.Font, brush, StringBounds);

      if (image != null)
      {
        Rectangle SourceRect = new Rectangle(0, 0, 253,174);
        g.DrawImage(image, 223,0,SourceRect,GraphicsUnit.Pixel);
      }

      if (commentBackgroundImage != null)
      {
        Rectangle SourceRect = new Rectangle(0, 0, 210, 23);
        g.DrawImage(commentBackgroundImage, 1, 161, SourceRect, GraphicsUnit.Pixel);
      }

      brush.Color = commentForeColor;

      StringBounds = new RectangleF(5, 165, 200, 20);
      g.DrawString(comment1Label + comment1, commentFont, brush, StringBounds);
    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private Image image = null;
    private Image commentBackgroundImage = null;
    private string comment1Label = string.Empty;
    private string comment1 = string.Empty;
    private Font commentFont = new Font("Tahoma", 10F, FontStyle.Bold);
    private Color commentForeColor = Color.White;
  }
}