namespace Slave.TranslatorPlugin
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class MainForm
	{		
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button uxTranslateButton;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.ComboBox uxLpComboBox;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.ComponentModel.BackgroundWorker uxBackgroundWorker;

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton uxFrenchToEnglishToolStripButton;
		private System.Windows.Forms.ToolStripButton uxEnglishToFrenchToolStripButton;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;
				
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			var resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.uxTranslateButton = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.uxLpComboBox = new System.Windows.Forms.ComboBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.uxBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.uxFrenchToEnglishToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.uxEnglishToFrenchToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 74);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(356, 96);
			this.textBox1.TabIndex = 0;
			this.toolTip1.SetToolTip(this.textBox1, "Type here your text to translate.\r\nUse CTL-Enter to start the translation\r\n");
			this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
			// 
			// uxTranslateButton
			// 
			this.uxTranslateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.uxTranslateButton.Location = new System.Drawing.Point(293, 174);
			this.uxTranslateButton.Name = "uxTranslateButton";
			this.uxTranslateButton.Size = new System.Drawing.Size(75, 23);
			this.uxTranslateButton.TabIndex = 1;
			this.uxTranslateButton.Text = "&Translate";
			this.uxTranslateButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox2
			// 
			this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox2.Location = new System.Drawing.Point(12, 203);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ReadOnly = true;
			this.textBox2.Size = new System.Drawing.Size(352, 160);
			this.textBox2.TabIndex = 2;
			// 
			// uxLpComboBox
			// 
			this.uxLpComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.uxLpComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.uxLpComboBox.FormattingEnabled = true;
			this.uxLpComboBox.Location = new System.Drawing.Point(12, 176);
			this.uxLpComboBox.Name = "uxLpComboBox";
			this.uxLpComboBox.Size = new System.Drawing.Size(275, 21);
			this.uxLpComboBox.TabIndex = 3;
			this.uxLpComboBox.SelectedIndexChanged += new System.EventHandler(this.uxLpComboBox_SelectedIndexChanged);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1});
			this.statusStrip1.Location = new System.Drawing.Point(0, 377);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(380, 22);
			this.statusStrip1.TabIndex = 4;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
			this.toolStripStatusLabel1.Text = "Ready";
			// 
			// toolStripProgressBar1
			// 
			this.toolStripProgressBar1.Name = "toolStripProgressBar1";
			this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
			this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
			this.toolStripProgressBar1.Visible = false;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(380, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.exitToolStripMenuItem.Text = "&Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "&Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStripMenuItem.Text = "&About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// uxBackgroundWorker
			// 
			this.uxBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.uxBackgroundWorker_DoWork);
			this.uxBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.uxBackgroundWorker_RunWorkerCompleted);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uxFrenchToEnglishToolStripButton,
            this.uxEnglishToFrenchToolStripButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new System.Drawing.Size(380, 36);
			this.toolStrip1.TabIndex = 6;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// uxFrenchToEnglishToolStripButton
			// 
			this.uxFrenchToEnglishToolStripButton.AutoToolTip = false;
			this.uxFrenchToEnglishToolStripButton.Image = global::Slave.TranslatorPlugin.Properties.Resources.fren;
			this.uxFrenchToEnglishToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxFrenchToEnglishToolStripButton.Name = "uxFrenchToEnglishToolStripButton";
			this.uxFrenchToEnglishToolStripButton.Size = new System.Drawing.Size(49, 33);
			this.uxFrenchToEnglishToolStripButton.Text = "fr -> en";
			this.uxFrenchToEnglishToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.uxFrenchToEnglishToolStripButton.ToolTipText = "From French to English";
			this.uxFrenchToEnglishToolStripButton.Click += new System.EventHandler(this.uxFrenchToEnglishToolStripButton_Click);
			// 
			// uxEnglishToFrenchToolStripButton
			// 
			this.uxEnglishToFrenchToolStripButton.AutoToolTip = false;
			this.uxEnglishToFrenchToolStripButton.Image = global::Slave.TranslatorPlugin.Properties.Resources.enfr;
			this.uxEnglishToFrenchToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.uxEnglishToFrenchToolStripButton.Name = "uxEnglishToFrenchToolStripButton";
			this.uxEnglishToFrenchToolStripButton.Size = new System.Drawing.Size(49, 33);
			this.uxEnglishToFrenchToolStripButton.Text = "en -> fr";
			this.uxEnglishToFrenchToolStripButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.uxEnglishToFrenchToolStripButton.ToolTipText = "From English to French";
			this.uxEnglishToFrenchToolStripButton.Click += new System.EventHandler(this.uxEnglishToFrenchToolStripButton_Click);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(380, 399);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.uxLpComboBox);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.uxTranslateButton);
			this.Controls.Add(this.textBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "SerialCoder Google Translator";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		}
}
