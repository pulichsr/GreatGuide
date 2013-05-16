namespace Nucleo.GoodGuide.CarApplication
{
  partial class frmGeoBaseConfiguration
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
      this.lblNotificationsMetadata = new System.Windows.Forms.Label();
      this.edtNotificationsMetadata = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.edtNotificationsTimeout = new System.Windows.Forms.TextBox();
      this.label2 = new System.Windows.Forms.Label();
      this.label3 = new System.Windows.Forms.Label();
      this.edtRoutingStrategy = new System.Windows.Forms.TextBox();
      this.chkLogging = new System.Windows.Forms.CheckBox();
      this.label4 = new System.Windows.Forms.Label();
      this.inputPanel1 = new Microsoft.WindowsCE.Forms.InputPanel();
      this.label5 = new System.Windows.Forms.Label();
      this.chkIsDepartureNotificationActive = new System.Windows.Forms.CheckBox();
      this.label6 = new System.Windows.Forms.Label();
      this.chkIsAfterTurnDistanceNotificationActive = new System.Windows.Forms.CheckBox();
      this.label7 = new System.Windows.Forms.Label();
      this.chkIsRoundaboutExitNotificationActive = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // lblNotificationsMetadata
      // 
      this.lblNotificationsMetadata.Location = new System.Drawing.Point(2, 13);
      this.lblNotificationsMetadata.Name = "lblNotificationsMetadata";
      this.lblNotificationsMetadata.Size = new System.Drawing.Size(84, 20);
      this.lblNotificationsMetadata.Text = "Notifications";
      // 
      // edtNotificationsMetadata
      // 
      this.edtNotificationsMetadata.Location = new System.Drawing.Point(83, 10);
      this.edtNotificationsMetadata.Name = "edtNotificationsMetadata";
      this.edtNotificationsMetadata.Size = new System.Drawing.Size(392, 23);
      this.edtNotificationsMetadata.TabIndex = 9;
      this.edtNotificationsMetadata.GotFocus += new System.EventHandler(this.edit_GotFocus);
      this.edtNotificationsMetadata.LostFocus += new System.EventHandler(this.edit_LostFocus);
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(2, 133);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(137, 20);
      this.label1.Text = "Notifications Timeout";
      // 
      // edtNotificationsTimeout
      // 
      this.edtNotificationsTimeout.Location = new System.Drawing.Point(145, 130);
      this.edtNotificationsTimeout.MaxLength = 3;
      this.edtNotificationsTimeout.Name = "edtNotificationsTimeout";
      this.edtNotificationsTimeout.Size = new System.Drawing.Size(48, 23);
      this.edtNotificationsTimeout.TabIndex = 12;
      this.edtNotificationsTimeout.GotFocus += new System.EventHandler(this.edit_GotFocus);
      this.edtNotificationsTimeout.LostFocus += new System.EventHandler(this.edit_LostFocus);
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(195, 133);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(42, 20);
      this.label2.Text = "secs";
      // 
      // label3
      // 
      this.label3.Location = new System.Drawing.Point(2, 163);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(117, 20);
      this.label3.Text = "Routing Strategy";
      // 
      // edtRoutingStrategy
      // 
      this.edtRoutingStrategy.Location = new System.Drawing.Point(145, 160);
      this.edtRoutingStrategy.MaxLength = 1;
      this.edtRoutingStrategy.Name = "edtRoutingStrategy";
      this.edtRoutingStrategy.Size = new System.Drawing.Size(48, 23);
      this.edtRoutingStrategy.TabIndex = 17;
      this.edtRoutingStrategy.GotFocus += new System.EventHandler(this.edit_GotFocus);
      this.edtRoutingStrategy.LostFocus += new System.EventHandler(this.edit_LostFocus);
      // 
      // chkLogging
      // 
      this.chkLogging.Location = new System.Drawing.Point(145, 190);
      this.chkLogging.Name = "chkLogging";
      this.chkLogging.Size = new System.Drawing.Size(100, 20);
      this.chkLogging.TabIndex = 22;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(2, 191);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(117, 20);
      this.label4.Text = "Logging";
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(2, 41);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(140, 20);
      this.label5.Text = "Departure";
      // 
      // chkIsDepartureNotificationActive
      // 
      this.chkIsDepartureNotificationActive.Location = new System.Drawing.Point(145, 40);
      this.chkIsDepartureNotificationActive.Name = "chkIsDepartureNotificationActive";
      this.chkIsDepartureNotificationActive.Size = new System.Drawing.Size(100, 20);
      this.chkIsDepartureNotificationActive.TabIndex = 28;
      // 
      // label6
      // 
      this.label6.Location = new System.Drawing.Point(2, 71);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(140, 20);
      this.label6.Text = "After Turn";
      // 
      // chkIsAfterTurnDistanceNotificationActive
      // 
      this.chkIsAfterTurnDistanceNotificationActive.Location = new System.Drawing.Point(145, 70);
      this.chkIsAfterTurnDistanceNotificationActive.Name = "chkIsAfterTurnDistanceNotificationActive";
      this.chkIsAfterTurnDistanceNotificationActive.Size = new System.Drawing.Size(100, 20);
      this.chkIsAfterTurnDistanceNotificationActive.TabIndex = 31;
      // 
      // label7
      // 
      this.label7.Location = new System.Drawing.Point(3, 101);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(140, 20);
      this.label7.Text = "Roundabout Exit";
      // 
      // chkIsRoundaboutExitNotificationActive
      // 
      this.chkIsRoundaboutExitNotificationActive.Location = new System.Drawing.Point(145, 100);
      this.chkIsRoundaboutExitNotificationActive.Name = "chkIsRoundaboutExitNotificationActive";
      this.chkIsRoundaboutExitNotificationActive.Size = new System.Drawing.Size(100, 20);
      this.chkIsRoundaboutExitNotificationActive.TabIndex = 34;
      // 
      // frmGeoBaseConfiguration
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.AutoScroll = true;
      this.ClientSize = new System.Drawing.Size(478, 247);
      this.Controls.Add(this.label7);
      this.Controls.Add(this.chkIsRoundaboutExitNotificationActive);
      this.Controls.Add(this.label6);
      this.Controls.Add(this.chkIsAfterTurnDistanceNotificationActive);
      this.Controls.Add(this.label5);
      this.Controls.Add(this.chkIsDepartureNotificationActive);
      this.Controls.Add(this.label4);
      this.Controls.Add(this.chkLogging);
      this.Controls.Add(this.edtRoutingStrategy);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.edtNotificationsTimeout);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.edtNotificationsMetadata);
      this.Controls.Add(this.lblNotificationsMetadata);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "frmGeoBaseConfiguration";
      this.Text = "GeoBase Configuration";
      this.Load += new System.EventHandler(this.frmGeoBaseConfiguration_Load);
      this.Closing += new System.ComponentModel.CancelEventHandler(this.frmGeoBaseConfiguration_Closing);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label lblNotificationsMetadata;
    private System.Windows.Forms.TextBox edtNotificationsMetadata;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox edtNotificationsTimeout;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox edtRoutingStrategy;
    private System.Windows.Forms.CheckBox chkLogging;
    private System.Windows.Forms.Label label4;
    private Microsoft.WindowsCE.Forms.InputPanel inputPanel1;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox chkIsDepartureNotificationActive;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.CheckBox chkIsAfterTurnDistanceNotificationActive;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.CheckBox chkIsRoundaboutExitNotificationActive;
  }
}