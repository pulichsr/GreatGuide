namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmConfirmStreet
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
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnNext = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lblMainHeading = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
      this.lblStreet = new System.Windows.Forms.Label();
      this.lblRegion = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnBack.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnBack.ForeColor = System.Drawing.Color.White;
      this.btnBack.LineSeparation = ((short)(1));
      this.btnBack.Location = new System.Drawing.Point(4, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(59, 42);
      this.btnBack.TabIndex = 52;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // btnNext
      // 
      this.btnNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnNext.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnNext.ForeColor = System.Drawing.Color.White;
      this.btnNext.LineSeparation = ((short)(1));
      this.btnNext.Location = new System.Drawing.Point(417, 2);
      this.btnNext.Name = "btnNext";
      this.btnNext.Size = new System.Drawing.Size(59, 42);
      this.btnNext.TabIndex = 84;
      this.btnNext.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lblMainHeading
      // 
      this.lblMainHeading.BackColor = System.Drawing.Color.White;
      this.lblMainHeading.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
      this.lblMainHeading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblMainHeading.Location = new System.Drawing.Point(71, 3);
      this.lblMainHeading.Name = "lblMainHeading";
      this.lblMainHeading.Size = new System.Drawing.Size(338, 23);
      this.lblMainHeading.TabIndex = 87;
      this.lblMainHeading.Text = "Confirm Street";
      this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblStreet
      // 
      this.lblStreet.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
      this.lblStreet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblStreet.Location = new System.Drawing.Point(2, 62);
      this.lblStreet.Name = "lblStreet";
      this.lblStreet.Size = new System.Drawing.Size(476, 43);
      this.lblStreet.Text = "street";
      this.lblStreet.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // lblRegion
      // 
      this.lblRegion.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Bold);
      this.lblRegion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblRegion.Location = new System.Drawing.Point(2, 124);
      this.lblRegion.Name = "lblRegion";
      this.lblRegion.Size = new System.Drawing.Size(476, 101);
      this.lblRegion.Text = "region";
      this.lblRegion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmConfirmStreet
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblRegion);
      this.Controls.Add(this.lblStreet);
      this.Controls.Add(this.lblMainHeading);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmConfirmStreet";
      this.Text = "frmDisclaimer";
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnNext;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblMainHeading;
    private System.Windows.Forms.Label lblStreet;
    private System.Windows.Forms.Label lblRegion;

  }
}