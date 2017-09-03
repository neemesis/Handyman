namespace Handyman.Core.Forms
{
	partial class Launcher
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer _components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (_components != null))
			{
				_components.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.uxInputText = new System.Windows.Forms.TextBox();
            this.uxInputContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uxNewHandymanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxHandymansToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.uxSetupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxHideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.uxExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uxInputContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // uxInputText
            // 
            this.uxInputText.AcceptsTab = true;
            this.uxInputText.BackColor = global::Handyman.Core.Properties.Settings.Default.Backcolor;
            this.uxInputText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.uxInputText.ContextMenuStrip = this.uxInputContextMenuStrip;
            this.uxInputText.DataBindings.Add(new System.Windows.Forms.Binding("ForeColor", global::Handyman.Core.Properties.Settings.Default, "ForeColor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.uxInputText.DataBindings.Add(new System.Windows.Forms.Binding("BackColor", global::Handyman.Core.Properties.Settings.Default, "Backcolor", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.uxInputText.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uxInputText.ForeColor = global::Handyman.Core.Properties.Settings.Default.ForeColor;
            this.uxInputText.Location = new System.Drawing.Point(10, 10);
            this.uxInputText.Margin = new System.Windows.Forms.Padding(5);
            this.uxInputText.MaximumSize = new System.Drawing.Size(500, 60);
            this.uxInputText.Name = "uxInputText";
            this.uxInputText.Size = new System.Drawing.Size(450, 25);
            this.uxInputText.TabIndex = 0;
            this.uxInputText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.uxInputText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnInputTextBoxKeyUp);
            this.uxInputText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnInputTextBoxMouseDown);
            // 
            // uxInputContextMenuStrip
            // 
            this.uxInputContextMenuStrip.ImageScalingSize = new System.Drawing.Size(40, 40);
            this.uxInputContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxNewHandymanToolStripMenuItem,
            this.uxHandymansToolStripMenuItem,
            this.reloadToolStripMenuItem,
            this.toolStripSeparator3,
            this.uxSetupToolStripMenuItem,
            this.uxHelpToolStripMenuItem,
            this.uxHideToolStripMenuItem,
            this.toolStripSeparator1,
            this.uxExitToolStripMenuItem});
            this.uxInputContextMenuStrip.Name = "contextMenuStrip1";
            this.uxInputContextMenuStrip.Size = new System.Drawing.Size(175, 170);
            this.uxInputContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.uxInputContextMenuStrip_Opening);
            // 
            // uxNewHandymanToolStripMenuItem
            // 
            this.uxNewHandymanToolStripMenuItem.Name = "uxNewHandymanToolStripMenuItem";
            this.uxNewHandymanToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxNewHandymanToolStripMenuItem.Text = "New Handyman...";
            this.uxNewHandymanToolStripMenuItem.Click += new System.EventHandler(this.OnNewHandymanToolStripMenuItemClick);
            // 
            // uxHandymansToolStripMenuItem
            // 
            this.uxHandymansToolStripMenuItem.Name = "uxHandymansToolStripMenuItem";
            this.uxHandymansToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxHandymansToolStripMenuItem.Text = "Handymans";
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
            // 
            // uxSetupToolStripMenuItem
            // 
            this.uxSetupToolStripMenuItem.Name = "uxSetupToolStripMenuItem";
            this.uxSetupToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxSetupToolStripMenuItem.Text = "&Setup...";
            this.uxSetupToolStripMenuItem.Click += new System.EventHandler(this.OnSetupToolStripMenuItemClick);
            // 
            // uxHelpToolStripMenuItem
            // 
            this.uxHelpToolStripMenuItem.Name = "uxHelpToolStripMenuItem";
            this.uxHelpToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxHelpToolStripMenuItem.Text = "&Handymans online";
            this.uxHelpToolStripMenuItem.Click += new System.EventHandler(this.OnHelpToolStripMenuItemClick);
            // 
            // uxHideToolStripMenuItem
            // 
            this.uxHideToolStripMenuItem.Name = "uxHideToolStripMenuItem";
            this.uxHideToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxHideToolStripMenuItem.Text = "&Hide in Tray";
            this.uxHideToolStripMenuItem.Click += new System.EventHandler(this.OnHideToolStripMenuItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // uxExitToolStripMenuItem
            // 
            this.uxExitToolStripMenuItem.Name = "uxExitToolStripMenuItem";
            this.uxExitToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.uxExitToolStripMenuItem.Text = "&Exit";
            this.uxExitToolStripMenuItem.Click += new System.EventHandler(this.OnExitToolStripMenuItemClick);
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(239)))));
            this.ClientSize = new System.Drawing.Size(663, 316);
            this.ContextMenuStrip = this.uxInputContextMenuStrip;
            this.ControlBox = false;
            this.Controls.Add(this.uxInputText);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Location", global::Handyman.Core.Properties.Settings.Default, "Position", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Location = global::Handyman.Core.Properties.Settings.Default.Position;
            this.Name = "Launcher";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.Deactivate += new System.EventHandler(this.OnDeactivate);
            this.uxInputContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox uxInputText;
		private System.Windows.Forms.ContextMenuStrip uxInputContextMenuStrip;
		private System.Windows.Forms.ToolStripMenuItem uxExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem uxHandymansToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uxNewHandymanToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem uxHideToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uxSetupToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem uxHelpToolStripMenuItem;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
    }
}