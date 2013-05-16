namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSelectRegion
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
      Resco.Controls.SmartGrid.Column column1 = new Resco.Controls.SmartGrid.Column();
      this.btnBack = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnNext = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lstItems = new Resco.Controls.SmartGrid.SmartGrid();
      this.btnPreviousPage = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.btnNextPage = new Nucleo.GoodGuide.Controls.MultilineTextButton();
      this.lblMainHeading = new Nucleo.Windows.Forms.TransparentControls.TransparentLabel();
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
      this.btnNext.Visible = false;
      this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
      // 
      // lstItems
      // 
      this.lstItems.AlternatingBackColor = System.Drawing.Color.WhiteSmoke;
      this.lstItems.AlternatingForeColor = System.Drawing.Color.Black;
      this.lstItems.BackColor = System.Drawing.Color.White;
      this.lstItems.BackgroundColor = System.Drawing.Color.White;
      this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.lstItems.ColumnHeadersVisible = false;
      column1.GridLine = false;
      column1.Width = 410;
      this.lstItems.Columns.Add(column1);
      this.lstItems.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
      this.lstItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lstItems.GridLineColor = System.Drawing.Color.Gainsboro;
      this.lstItems.GridLines = false;
      this.lstItems.Location = new System.Drawing.Point(2, 50);
      this.lstItems.Name = "lstItems";
      this.lstItems.Rows.Add(new Resco.Controls.SmartGrid.Row(21, new string[] {
                "Loading..."}));
      this.lstItems.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(94)))), ((int)(((byte)(113)))));
      this.lstItems.SelectionForeColor = System.Drawing.Color.White;
      this.lstItems.Size = new System.Drawing.Size(412, 219);
      this.lstItems.TabIndex = 10;
      this.lstItems.SelectionChanged += new System.EventHandler(this.lstItems_SelectionChanged);
      // 
      // btnPreviousPage
      // 
      this.btnPreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnPreviousPage.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnPreviousPage.ForeColor = System.Drawing.Color.White;
      this.btnPreviousPage.LineSeparation = ((short)(0));
      this.btnPreviousPage.Location = new System.Drawing.Point(417, 50);
      this.btnPreviousPage.Name = "btnPreviousPage";
      this.btnPreviousPage.Size = new System.Drawing.Size(40, 37);
      this.btnPreviousPage.TabIndex = 85;
      this.btnPreviousPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnPreviousPage.Visible = false;
      this.btnPreviousPage.Click += new System.EventHandler(this.btnPreviousPage_Click);
      // 
      // btnNextPage
      // 
      this.btnNextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.btnNextPage.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.btnNextPage.ForeColor = System.Drawing.Color.White;
      this.btnNextPage.LineSeparation = ((short)(0));
      this.btnNextPage.Location = new System.Drawing.Point(417, 232);
      this.btnNextPage.Name = "btnNextPage";
      this.btnNextPage.Size = new System.Drawing.Size(40, 37);
      this.btnNextPage.TabIndex = 86;
      this.btnNextPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      this.btnNextPage.Visible = false;
      this.btnNextPage.Click += new System.EventHandler(this.btnNextPage_Click);
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
      this.lblMainHeading.Text = "Select Region";
      this.lblMainHeading.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // frmSelectRegion
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblMainHeading);
      this.Controls.Add(this.btnPreviousPage);
      this.Controls.Add(this.btnNextPage);
      this.Controls.Add(this.lstItems);
      this.Controls.Add(this.btnNext);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmSelectRegion";
      this.Text = "frmDisclaimer";
      this.Load += new System.EventHandler(this.frmSelectRegion_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmSelectRegion_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private Nucleo.GoodGuide.Controls.MultilineTextButton btnBack;
    private Nucleo.GoodGuide.Controls.MultilineTextButton btnNext;
    private Resco.Controls.SmartGrid.SmartGrid lstItems;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnPreviousPage;
    public Nucleo.GoodGuide.Controls.MultilineTextButton btnNextPage;
    private Nucleo.Windows.Forms.TransparentControls.TransparentLabel lblMainHeading;

  }
}