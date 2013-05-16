namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmSelectPoiDestination
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
      Resco.Controls.SmartGrid.Column column2 = new Resco.Controls.SmartGrid.Column();
      this.sgData = new Resco.Controls.SmartGrid.SmartGrid();
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
      column1.DataMember = "DistanceText";
      column1.Width = 100;
      column2.DataMember = "Code";
      column2.Width = 368;
      this.sgData.Columns.Add(column1);
      this.sgData.Columns.Add(column2);
      this.sgData.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
      this.sgData.ForeColor = System.Drawing.Color.White;
      this.sgData.FullRowSelect = true;
      this.sgData.GridLineColor = System.Drawing.Color.White;
      this.sgData.Location = new System.Drawing.Point(5, 45);
      this.sgData.Name = "sgData";
      this.sgData.ScrollWidth = 0;
      this.sgData.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(94)))), ((int)(((byte)(113)))));
      this.sgData.SelectionForeColor = System.Drawing.Color.White;
      this.sgData.Size = new System.Drawing.Size(470, 180);
      this.sgData.TabIndex = 0;
      // 
      // frmSelectPoiDestination
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.sgData);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmSelectPoiDestination";
      this.Text = "frmSelectPoiCatagory";
      this.ResumeLayout(false);

    }

    #endregion

    private Resco.Controls.SmartGrid.SmartGrid sgData;
  }
}