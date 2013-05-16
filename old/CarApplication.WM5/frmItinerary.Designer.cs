namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmItinerary
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
      this.lblDate = new System.Windows.Forms.Label();
      this.lblDay = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // lblDate
      // 
      this.lblDate.BackColor = System.Drawing.Color.White;
      this.lblDate.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblDate.Location = new System.Drawing.Point(73, 24);
      this.lblDate.Name = "lblDate";
      this.lblDate.Size = new System.Drawing.Size(113, 20);
      // 
      // lblDay
      // 
      this.lblDay.BackColor = System.Drawing.Color.White;
      this.lblDay.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblDay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblDay.Location = new System.Drawing.Point(192, 24);
      this.lblDay.Name = "lblDay";
      this.lblDay.Size = new System.Drawing.Size(88, 20);
      // 
      // frmItinerary
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblDay);
      this.Controls.Add(this.lblDate);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmItinerary";
      this.Text = "Destination List";
      this.Activated += new System.EventHandler(this.frmItinerary_Activated);
      this.Load += new System.EventHandler(this.frmItinerary_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblDate;
    private System.Windows.Forms.Label lblDay;
  }
}