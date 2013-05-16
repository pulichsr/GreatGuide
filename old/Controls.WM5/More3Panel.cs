using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleo.GoodGuide.Controls
{
  public class More3Panel : Control
  {
    public More3Panel()
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
    public Image CommentBackgroundImage
    {
      set
      {
        commentBackgroundImage = value;
        Invalidate();
      }
    }
    public Image NumberBackgroundImage
    {
      set
      {
        numberBackgroundImage = value;
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
    public string Comment2Label
    {
      get { return comment2Label; }
      set
      {
        comment2Label = value;
        Invalidate();
      }
    }
    public string Comment3Label
    {
      get { return comment3Label; }
      set
      {
        comment3Label = value;
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
    public string Comment2
    {
      get { return comment2; }
      set
      {
        comment2 = value;
        Invalidate();
      }
    }
    public string Comment3
    {
      get { return comment3; }
      set
      {
        comment3 = value;
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
    public string TelNo
    {
      get { return telNo; }
      set { telNo = value; }
    }
    public string CellNo
    {
      get { return cellNo; }
      set { cellNo = value; }
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

      #region Text
      brush.Color = this.ForeColor;
      RectangleF StringBounds = new RectangleF(4, 0, 250, 114);
      g.DrawString(Text, this.Font, brush, StringBounds);
      #endregion

      #region Comment background
      if (commentBackgroundImage != null)
      {
        Rectangle SourceRect = new Rectangle(0, 0, 476, 23);
        g.DrawImage(commentBackgroundImage, 1, 115, SourceRect, GraphicsUnit.Pixel);
        g.DrawImage(commentBackgroundImage, 1, 138, SourceRect, GraphicsUnit.Pixel);
        g.DrawImage(commentBackgroundImage, 1, 161, SourceRect, GraphicsUnit.Pixel);
      }
      else
      {
        SolidBrush CommentBrush = new SolidBrush(Color.Aqua);
        Rectangle Rect1 = new Rectangle(1, 115, 476, 23);
        g.FillRectangle(CommentBrush,Rect1);

        Rectangle Rect2 = new Rectangle(1, 138, 476, 23);
        g.FillRectangle(CommentBrush, Rect2);

        Rectangle Rect3 = new Rectangle(1, 161, 476, 23);
        g.FillRectangle(CommentBrush, Rect3);
      }
      #endregion

      #region Comment text
      brush.Color = commentForeColor;
      StringBounds = new RectangleF(5, 118, 465, 20);
      g.DrawString(comment1Label + comment1, commentFont, brush, StringBounds);

      StringBounds = new RectangleF(5, 141, 465, 20);
      g.DrawString(comment2Label + comment2, commentFont, brush, StringBounds);

      StringBounds = new RectangleF(5, 164, 465, 20);
      g.DrawString(comment3Label + comment3, commentFont, brush, StringBounds);
      #endregion

      #region Number background
      if (numberBackgroundImage != null)
      {
        Rectangle SourceRect = new Rectangle(0, 0, 210, 23);
        g.DrawImage(numberBackgroundImage, 260, 0, SourceRect, GraphicsUnit.Pixel);
        g.DrawImage(numberBackgroundImage, 260, 23, SourceRect, GraphicsUnit.Pixel);
      }
      else
      {
        SolidBrush NumberBrush = new SolidBrush(Color.Aqua);
        Rectangle Rect1 = new Rectangle(260, 0, 210, 23);
        g.FillRectangle(NumberBrush, Rect1);

        Rectangle Rect2 = new Rectangle(260, 23, 210, 23);
        g.FillRectangle(NumberBrush, Rect2);
      }
      #endregion

      #region Number text
      brush.Color = commentForeColor;
      StringBounds = new RectangleF(265, 3, 210, 20);
      g.DrawString("Tel No: " + telNo, commentFont, brush, StringBounds);

      StringBounds = new RectangleF(265, 26, 210, 20);
      g.DrawString("Cell No: " + cellNo, commentFont, brush, StringBounds);
      #endregion

    }

    private Bitmap bitmap;
    private Pen pen;
    private SolidBrush brush;
    private Image commentBackgroundImage = null;
    private Image numberBackgroundImage = null;
    private string comment1Label = string.Empty;
    private string comment2Label = string.Empty;
    private string comment3Label = string.Empty;
    private string comment1 = string.Empty;
    private string comment2 = string.Empty;
    private string comment3 = string.Empty;
    private string telNo = string.Empty;
    private string cellNo = string.Empty;
    private Font commentFont = new Font("Tahoma", 10F, FontStyle.Bold);
    private Color commentForeColor = Color.White;
  }
}