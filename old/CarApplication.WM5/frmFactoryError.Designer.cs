namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmFactoryError
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
      this.odFile = new System.Windows.Forms.OpenFileDialog();
      this.btnBack = new System.Windows.Forms.Button();
      this.edtText = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // odFile
      // 
      this.odFile.Filter = "Data Files (*.xml)|*.xml";
      // 
      // btnBack
      // 
      this.btnBack.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.btnBack.Location = new System.Drawing.Point(405, 231);
      this.btnBack.Name = "btnBack";
      this.btnBack.Size = new System.Drawing.Size(72, 38);
      this.btnBack.TabIndex = 9;
      this.btnBack.Text = "Back";
      this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
      // 
      // edtText
      // 
      this.edtText.Dock = System.Windows.Forms.DockStyle.Top;
      this.edtText.Location = new System.Drawing.Point(0, 0);
      this.edtText.Multiline = true;
      this.edtText.Name = "edtText";
      this.edtText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.edtText.Size = new System.Drawing.Size(480, 225);
      this.edtText.TabIndex = 10;
      // 
      // frmFactoryError
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(480, 272);
      this.ControlBox = false;
      this.Controls.Add(this.edtText);
      this.Controls.Add(this.btnBack);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmFactoryError";
      this.Text = "Load Data";
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.OpenFileDialog odFile;
    private System.Windows.Forms.Button btnBack;
    private System.Windows.Forms.TextBox edtText;
  }
}