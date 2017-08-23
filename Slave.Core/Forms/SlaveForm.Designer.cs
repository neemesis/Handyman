namespace Slave.Core.Forms
{
	partial class SlaveForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.uxAliasTextBox = new System.Windows.Forms.TextBox();
            this.uxFilenameTextBox = new System.Windows.Forms.TextBox();
            this.uxStartupModeComboBox = new System.Windows.Forms.ComboBox();
            this.uxArgumentsTextBox = new System.Windows.Forms.TextBox();
            this.uxNotesTextBox = new System.Windows.Forms.TextBox();
            this.uxStartupPathTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.uxAcceptButton = new System.Windows.Forms.Button();
            this.uxCancelButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.uxAliasTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.uxFilenameTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.uxStartupModeComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.uxArgumentsTextBox, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.uxNotesTextBox, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.uxStartupPathTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(468, 182);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 23);
            this.label6.TabIndex = 8;
            this.label6.Text = "Notes :";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 100);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Arguments :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Startup path :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(3, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 23);
            this.label3.TabIndex = 8;
            this.label3.Text = "Startup mode :";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Filename or url :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxAliasTextBox
            // 
            this.uxAliasTextBox.Location = new System.Drawing.Point(123, 3);
            this.uxAliasTextBox.Name = "uxAliasTextBox";
            this.uxAliasTextBox.Size = new System.Drawing.Size(342, 20);
            this.uxAliasTextBox.TabIndex = 0;
            // 
            // uxFilenameTextBox
            // 
            this.uxFilenameTextBox.Location = new System.Drawing.Point(123, 28);
            this.uxFilenameTextBox.Name = "uxFilenameTextBox";
            this.uxFilenameTextBox.Size = new System.Drawing.Size(342, 20);
            this.uxFilenameTextBox.TabIndex = 1;
            // 
            // uxStartupModeComboBox
            // 
            this.uxStartupModeComboBox.FormattingEnabled = true;
            this.uxStartupModeComboBox.Location = new System.Drawing.Point(123, 53);
            this.uxStartupModeComboBox.Name = "uxStartupModeComboBox";
            this.uxStartupModeComboBox.Size = new System.Drawing.Size(121, 21);
            this.uxStartupModeComboBox.TabIndex = 2;
            // 
            // uxArgumentsTextBox
            // 
            this.uxArgumentsTextBox.Location = new System.Drawing.Point(123, 103);
            this.uxArgumentsTextBox.Name = "uxArgumentsTextBox";
            this.uxArgumentsTextBox.Size = new System.Drawing.Size(342, 20);
            this.uxArgumentsTextBox.TabIndex = 5;
            // 
            // uxNotesTextBox
            // 
            this.uxNotesTextBox.Location = new System.Drawing.Point(123, 128);
            this.uxNotesTextBox.Multiline = true;
            this.uxNotesTextBox.Name = "uxNotesTextBox";
            this.uxNotesTextBox.Size = new System.Drawing.Size(342, 51);
            this.uxNotesTextBox.TabIndex = 4;
            // 
            // uxStartupPathTextBox
            // 
            this.uxStartupPathTextBox.Location = new System.Drawing.Point(123, 78);
            this.uxStartupPathTextBox.Name = "uxStartupPathTextBox";
            this.uxStartupPathTextBox.Size = new System.Drawing.Size(342, 20);
            this.uxStartupPathTextBox.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Slave name:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uxAcceptButton
            // 
            this.uxAcceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxAcceptButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.uxAcceptButton.Location = new System.Drawing.Point(324, 199);
            this.uxAcceptButton.Name = "uxAcceptButton";
            this.uxAcceptButton.Size = new System.Drawing.Size(75, 23);
            this.uxAcceptButton.TabIndex = 4;
            this.uxAcceptButton.Text = "&OK";
            this.uxAcceptButton.UseVisualStyleBackColor = true;
            // 
            // uxCancelButton
            // 
            this.uxCancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uxCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.uxCancelButton.Location = new System.Drawing.Point(405, 200);
            this.uxCancelButton.Name = "uxCancelButton";
            this.uxCancelButton.Size = new System.Drawing.Size(75, 23);
            this.uxCancelButton.TabIndex = 3;
            this.uxCancelButton.Text = "&Cancel";
            this.uxCancelButton.UseVisualStyleBackColor = true;
            // 
            // SlaveForm
            // 
            this.AcceptButton = this.uxAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.uxCancelButton;
            this.ClientSize = new System.Drawing.Size(493, 234);
            this.ControlBox = false;
            this.Controls.Add(this.uxAcceptButton);
            this.Controls.Add(this.uxCancelButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SlaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slave editor";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox uxAliasTextBox;
		private System.Windows.Forms.TextBox uxFilenameTextBox;
		private System.Windows.Forms.ComboBox uxStartupModeComboBox;
		private System.Windows.Forms.TextBox uxArgumentsTextBox;
		private System.Windows.Forms.TextBox uxNotesTextBox;
		private System.Windows.Forms.TextBox uxStartupPathTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button uxAcceptButton;
		private System.Windows.Forms.Button uxCancelButton;
	}
}