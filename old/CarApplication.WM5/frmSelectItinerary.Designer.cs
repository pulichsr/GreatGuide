namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSelectItinerary
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
      Resco.Controls.SmartGrid.Column column3 = new Resco.Controls.SmartGrid.Column();
      Resco.Controls.SmartGrid.Column column4 = new Resco.Controls.SmartGrid.Column();
      this.sgData = new Resco.Controls.SmartGrid.SmartGrid();
      this.btnDown = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnUp = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.keyboardButton5 = new Nucleo.GoodGuide.Controls.KeyboardButton();
      this.lblPrompt = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // sgData
      // 
      this.sgData.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.sgData.AlternatingForeColor = System.Drawing.Color.White;
      this.sgData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.sgData.BackgroundColor = System.Drawing.Color.White;
      this.sgData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.sgData.ColumnHeadersVisible = false;
      column3.DataMember = "Name";
      column3.Width = 222;
      column4.DataMember = "ArrivalDat";
      column4.Width = 200;
      this.sgData.Columns.Add(column3);
      this.sgData.Columns.Add(column4);
      this.sgData.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.sgData.ForeColor = System.Drawing.Color.White;
      this.sgData.FullRowSelect = true;
      this.sgData.GridLineColor = System.Drawing.Color.White;
      this.sgData.Location = new System.Drawing.Point(4, 46);
      this.sgData.Name = "sgData";
      this.sgData.ScrollWidth = 0;
      this.sgData.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(94)))), ((int)(((byte)(113)))));
      this.sgData.SelectionForeColor = System.Drawing.Color.White;
      this.sgData.Size = new System.Drawing.Size(424, 187);
      this.sgData.TabIndex = 49;
      // 
      // btnDown
      // 
      this.btnDown.BackColor = System.Drawing.SystemColors.Window;
      this.btnDown.ForeColor = System.Drawing.SystemColors.WindowText;
      this.btnDown.LineSeparation = ((short)(1));
      this.btnDown.Location = new System.Drawing.Point(434, 193);
      this.btnDown.Name = "btnDown";
      this.btnDown.Size = new System.Drawing.Size(40, 40);
      this.btnDown.TabIndex = 48;
      this.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
      // 
      // btnUp
      // 
      this.btnUp.BackColor = System.Drawing.SystemColors.Window;
      this.btnUp.ForeColor = System.Drawing.SystemColors.WindowText;
      this.btnUp.LineSeparation = ((short)(1));
      this.btnUp.Location = new System.Drawing.Point(434, 44);
      this.btnUp.Name = "btnUp";
      this.btnUp.Size = new System.Drawing.Size(40, 40);
      this.btnUp.TabIndex = 50;
      this.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
      // 
      // btnBack
      // 
      this.btnBack.BackColor = System.Drawing.SystemColors.Window;
      this.btnBack.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
      this.btnBack.ForeColor = System.Drawing.Color.White;
      this.btnBack.LineSeparation = ((short)(1));
      this.btnBack.Location = new System.Drawing.Point(4, 2);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(59, 42);
      this.btnBack.TabIndex = 52;
      this.btnBack.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // keyboardButton5
      // 
      this.keyboardButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.keyboardButton5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
      this.keyboardButton5.ForeColor = System.Drawing.Color.White;
      this.keyboardButton5.Location = new System.Drawing.Point(386, 239);
      this.keyboardButton5.Name = "keyboardButton5";
      this.keyboardButton5.Size = new System.Drawing.Size(88, 30);
      this.keyboardButton5.TabIndex = 77;
      this.keyboardButton5.Text = "OK";
      this.keyboardButton5.Click += new System.EventHandler(this.btnOK_Click);
      // 
      // lblPrompt
      // 
      this.lblPrompt.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblPrompt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(136)))), ((int)(((byte)(35)))));
      this.lblPrompt.Location = new System.Drawing.Point(62, 2);
      this.lblPrompt.Name = "lblPrompt";
      this.lblPrompt.Size = new System.Drawing.Size(346, 20);
      this.lblPrompt.Text = "Select itinerary to load";
      this.lblPrompt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmSelectItinerary
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblPrompt);
      this.Controls.Add(this.keyboardButton5);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.btnUp);
      this.Controls.Add(this.sgData);
      this.Controls.Add(this.btnDown);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSelectItinerary";
      this.Text = "frmDisclaimer";
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnDown;
    private Resco.Controls.SmartGrid.SmartGrid sgData;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnUp;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private Nucleo.GoodGuide.Controls.KeyboardButton keyboardButton5;
    private System.Windows.Forms.Label lblPrompt;

  }
}