namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmMessage
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
      this.keyboardButton2 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.lblText = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // keyboardButton2
      // 
      this.keyboardButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton2.ForeColor = System.Drawing.Color.White;
      this.keyboardButton2.Location = new System.Drawing.Point(386, 239);
      this.keyboardButton2.Name = "keyboardButton2";
      this.keyboardButton2.Size = new System.Drawing.Size(88, 30);
      this.keyboardButton2.TabIndex = 77;
      this.keyboardButton2.Text = "OK";
      this.keyboardButton2.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // lblText
      // 
      this.lblText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.lblText.Location = new System.Drawing.Point(2, 13);
      this.lblText.Name = "lblText";
      this.lblText.Size = new System.Drawing.Size(476, 220);
      // 
      // frmMessage
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblText);
      this.Controls.Add(this.keyboardButton2);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmMessage";
      this.Text = "frmDisclaimer";
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton2;
    private System.Windows.Forms.Label lblText;

  }
}