namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmCommentMore
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.edtComment = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // edtComment
      // 
      this.edtComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.edtComment.Location = new System.Drawing.Point(2, 53);
      this.edtComment.Multiline = true;
      this.edtComment.Name = "edtComment";
      this.edtComment.Size = new System.Drawing.Size(476, 219);
      this.edtComment.TabIndex = 0;
      // 
      // frmCommentMore
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtComment);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmCommentMore";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox edtComment;

  }
}