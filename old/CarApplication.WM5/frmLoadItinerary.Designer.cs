namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmLoadItinerary
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
      this.btnLoadFile = new System.Windows.Forms.Button();
      this.lblFileStep = new System.Windows.Forms.Label();
      this.btnBack = new System.Windows.Forms.Button();
      this.label3 = new System.Windows.Forms.Label();
      this.edtCurrentFile = new System.Windows.Forms.TextBox();
      this.lblUrlStep = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnLoadFile
      // 
      this.btnLoadFile.Location = new System.Drawing.Point(236, 19);
      this.btnLoadFile.Name = "btnLoadFile";
      this.btnLoadFile.Size = new System.Drawing.Size(50, 20);
      this.btnLoadFile.TabIndex = 6;
      this.btnLoadFile.Text = "Load";
      this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
      // 
      // lblFileStep
      // 
      this.lblFileStep.Location = new System.Drawing.Point(3, 109);
      this.lblFileStep.Name = "lblFileStep";
      this.lblFileStep.Size = new System.Drawing.Size(477, 20);
      // 
      // btnBack
      // 
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnBack.Location = new System.Drawing.Point(405, 231);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(72, 38);
      this.btnBack.TabIndex = 9;
      this.btnBack.Text = "Back";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(3, 0);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(188, 20);
      this.label3.Text = "Current itinerary file";
      // 
      // edtCurrentFile
      // 
      this.edtCurrentFile.Location = new System.Drawing.Point(3, 18);
      this.edtCurrentFile.Name = "edtCurrentFile";
      this.edtCurrentFile.ReadOnly = true;
      this.edtCurrentFile.Size = new System.Drawing.Size(227, 23);
      this.edtCurrentFile.TabIndex = 19;
      // 
      // lblUrlStep
      // 
      this.lblUrlStep.Location = new System.Drawing.Point(3, 201);
      this.lblUrlStep.Name = "lblUrlStep";
      this.lblUrlStep.Size = new System.Drawing.Size(477, 20);
      // 
      // frmLoadItinerary
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.lblUrlStep);
      this.Controls.Add(this.edtCurrentFile);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.btnBack);
      this.Controls.Add(this.lblFileStep);
      this.Controls.Add(this.btnLoadFile);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmLoadItinerary";
      this.Text = "Load Data";
      this.Load += new System.EventHandler(this.frmLoadData_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmLoadData_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button btnLoadFile;
    private System.Windows.Forms.Label lblFileStep;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtCurrentFile;
    private System.Windows.Forms.Label lblUrlStep;
  }
}