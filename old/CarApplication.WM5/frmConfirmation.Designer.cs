namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmConfirmation
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
      this.btnYes = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.btnNo = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.lblText = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnYes
      // 
      this.btnYes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnYes.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnYes.ForeColor = System.Drawing.Color.White;
      this.btnYes.Location = new System.Drawing.Point(386, 239);
      this.btnYes.Name = "btnYes";
      this.btnYes.Size = new System.Drawing.Size(88, 30);
      this.btnYes.TabIndex = 77;
      this.btnYes.Text = "Yes";
      this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
      // 
      // btnNo
      // 
      this.btnNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.btnNo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.btnNo.ForeColor = System.Drawing.Color.White;
      this.btnNo.Location = new System.Drawing.Point(3, 239);
      this.btnNo.Name = "btnNo";
      this.btnNo.Size = new System.Drawing.Size(88, 30);
      this.btnNo.TabIndex = 78;
      this.btnNo.Text = "No";
      this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
      // 
      // lblText
      // 
      this.lblText.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.lblText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.lblText.Location = new System.Drawing.Point(5, 13);
      this.lblText.Name = "lblText";
      this.lblText.Size = new System.Drawing.Size(471, 154);
      this.lblText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmConfirmation
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblText);
      this.Controls.Add(this.btnNo);
      this.Controls.Add(this.btnYes);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmConfirmation";
      this.Text = "frmDisclaimer";
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.KeyboardButton btnYes;
    private Nucleo.GoodGuide.Controls.KeyboardButton btnNo;
    private System.Windows.Forms.Label lblText;

  }
}