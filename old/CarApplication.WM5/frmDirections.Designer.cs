
namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmDirections
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
      this.sgData = new Resco.Controls.SmartGrid.SmartGrid();
      this.SuspendLayout();
      // 
      // sgData
      // 
      this.sgData.AlternatingBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.sgData.AlternatingForeColor = System.Drawing.Color.White;
      this.sgData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.sgData.BackgroundColor = System.Drawing.Color.White;
      this.sgData.ColumnHeadersVisible = false;
      column1.Width = 476;
      this.sgData.Columns.Add(column1);
      this.sgData.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
      this.sgData.ForeColor = System.Drawing.Color.White;
      this.sgData.FullRowSelect = true;
      this.sgData.GridLineColor = System.Drawing.Color.White;
      this.sgData.Location = new System.Drawing.Point(1, 46);
      this.sgData.Name = "sgData";
      this.sgData.ScrollWidth = 0;
      this.sgData.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(94)))), ((int)(((byte)(113)))));
      this.sgData.SelectionForeColor = System.Drawing.Color.White;
      this.sgData.Size = new System.Drawing.Size(478, 186);
      this.sgData.TabIndex = 50;
      // 
      // frmDirections
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.sgData);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmDirections";
      this.Text = "Destination List";
      this.Load += new System.EventHandler(this.frmDirection_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private Resco.Controls.SmartGrid.SmartGrid sgData;

  }
}