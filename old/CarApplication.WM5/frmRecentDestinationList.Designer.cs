using Resco.Controls.AdvancedList;

namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmRecentDestinationList
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
      Resco.Controls.AdvancedList.RowTemplate rowTemplate2 = new Resco.Controls.AdvancedList.RowTemplate();
      Resco.Controls.AdvancedList.TextCell textCell2 = new Resco.Controls.AdvancedList.TextCell();
      Resco.Controls.AdvancedList.SeparatorCell separatorCell2 = new Resco.Controls.AdvancedList.SeparatorCell();
      this.alData = new Resco.Controls.AdvancedList.AdvancedList();
      this.lblPage = new System.Windows.Forms.Label();
      this.ilDestinationType = new System.Windows.Forms.ImageList();
      this.ilInfoLeft = new System.Windows.Forms.ImageList();
      this.ilInfoRight = new System.Windows.Forms.ImageList();
      this.ilMore = new System.Windows.Forms.ImageList();
      this.SuspendLayout();
      // 
      // alData
      // 
      this.alData.BackColor = System.Drawing.Color.White;
      this.alData.Location = new System.Drawing.Point(5, 45);
      this.alData.Name = "alData";
      this.alData.ScrollbarWidth = 1;
      this.alData.SelectedTemplateIndex = 2;
      this.alData.SelectionMode = Resco.Controls.AdvancedList.SelectionMode.NoSelect;
      this.alData.Size = new System.Drawing.Size(470, 189);
      this.alData.TabIndex = 0;
      this.alData.TemplateIndex = 1;
      rowTemplate2.BackColor = System.Drawing.SystemColors.Control;
      textCell2.Bounds = new System.Drawing.Rectangle(0, 0, -1, 16);
      textCell2.CellSource.ColumnName = "Code";
      separatorCell2.BackColor = System.Drawing.Color.DarkGray;
      separatorCell2.Bounds = new System.Drawing.Rectangle(0, 16, 100, 16);
      separatorCell2.SeparatorType = Resco.Controls.AdvancedList.SeparatorType.Empty;
      rowTemplate2.CellTemplates.Add(textCell2);
      rowTemplate2.CellTemplates.Add(separatorCell2);
      this.alData.Templates.Add(rowTemplate2);
      this.alData.CellClick += new Resco.Controls.AdvancedList.CellEventHandler(this.alData_CellClick);
      // 
      // lblPage
      // 
      this.lblPage.BackColor = System.Drawing.Color.White;
      this.lblPage.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
      this.lblPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(123)))), ((int)(((byte)(16)))));
      this.lblPage.Location = new System.Drawing.Point(305, 22);
      this.lblPage.Name = "lblPage";
      this.lblPage.Size = new System.Drawing.Size(91, 20);
      this.lblPage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
      // 
      // ilDestinationType
      // 
      this.ilDestinationType.ImageSize = new System.Drawing.Size(57, 45);
      // 
      // ilInfoLeft
      // 
      this.ilInfoLeft.ImageSize = new System.Drawing.Size(255, 45);
      // 
      // ilInfoRight
      // 
      this.ilInfoRight.ImageSize = new System.Drawing.Size(34, 45);
      // 
      // ilMore
      // 
      this.ilMore.ImageSize = new System.Drawing.Size(114, 45);
      // 
      // frmDestinationListBase
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblPage);
      this.Controls.Add(this.alData);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.Name = "frmRecentDestinationList";
      this.Load += new System.EventHandler(this.frmRecentDestinationList_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private Resco.Controls.AdvancedList.AdvancedList alData;
    private System.Windows.Forms.Label lblPage;
    private System.Windows.Forms.ImageList ilDestinationType;
    private System.Windows.Forms.ImageList ilInfoLeft;
    private System.Windows.Forms.ImageList ilInfoRight;
    private System.Windows.Forms.ImageList ilMore;
  }
}