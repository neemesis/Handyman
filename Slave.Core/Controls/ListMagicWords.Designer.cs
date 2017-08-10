namespace Slave.Core.Controls
{
	partial class ListSlaves
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.uxDataGridView = new System.Windows.Forms.DataGridView();
			this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.uxAliasColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxFilenameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxArgumentsColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.uxModesColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.uxNotesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.uxDataGridView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// uxDataGridView
			// 
			this.uxDataGridView.AutoGenerateColumns = false;
			this.uxDataGridView.BackgroundColor = System.Drawing.Color.WhiteSmoke;
			this.uxDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.uxDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.uxAliasColumn,
            this.uxFilenameColumn,
            this.uxArgumentsColumn,
            this.uxModesColumn,
            this.uxNotesColumn});
			this.uxDataGridView.DataSource = this.bindingSource1;
			this.uxDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.uxDataGridView.EnableHeadersVisualStyles = false;
			this.uxDataGridView.Location = new System.Drawing.Point(0, 0);
			this.uxDataGridView.Name = "uxDataGridView";
			this.uxDataGridView.Size = new System.Drawing.Size(516, 292);
			this.uxDataGridView.TabIndex = 1;
			this.uxDataGridView.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.OnDataGridViewCellFormatting);
			this.uxDataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.OnDataGridViewCellParsing);
			this.uxDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.OnDataGridViewDataError);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			this.errorProvider1.DataSource = this.bindingSource1;
			// 
			// uxAliasColumn
			// 
			this.uxAliasColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.uxAliasColumn.DataPropertyName = "Alias";
			this.uxAliasColumn.HeaderText = "Magic word";
			this.uxAliasColumn.MinimumWidth = 90;
			this.uxAliasColumn.Name = "uxAliasColumn";
			this.uxAliasColumn.Width = 90;
			// 
			// uxFilenameColumn
			// 
			this.uxFilenameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.uxFilenameColumn.DataPropertyName = "FileName";
			this.uxFilenameColumn.HeaderText = "Filename";
			this.uxFilenameColumn.MinimumWidth = 90;
			this.uxFilenameColumn.Name = "uxFilenameColumn";
			this.uxFilenameColumn.Width = 90;
			// 
			// uxArgumentsColumn
			// 
			this.uxArgumentsColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.uxArgumentsColumn.DataPropertyName = "Arguments";
			this.uxArgumentsColumn.HeaderText = "Arguments";
			this.uxArgumentsColumn.MinimumWidth = 90;
			this.uxArgumentsColumn.Name = "uxArgumentsColumn";
			this.uxArgumentsColumn.Width = 90;
			// 
			// uxModesColumn
			// 
			this.uxModesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.uxModesColumn.DataPropertyName = "StartUpMode";
			this.uxModesColumn.HeaderText = "Startup mode";
			this.uxModesColumn.MinimumWidth = 100;
			this.uxModesColumn.Name = "uxModesColumn";
			// 
			// uxNotesColumn
			// 
			this.uxNotesColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
			this.uxNotesColumn.DataPropertyName = "Notes";
			this.uxNotesColumn.HeaderText = "Notes";
			this.uxNotesColumn.MinimumWidth = 90;
			this.uxNotesColumn.Name = "uxNotesColumn";
			this.uxNotesColumn.Width = 90;
			// 
			// ListSlaves
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.uxDataGridView);
			this.Name = "ListSlaves";
			this.Size = new System.Drawing.Size(516, 292);
			((System.ComponentModel.ISupportInitialize)(this.uxDataGridView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView uxDataGridView;
		private System.Windows.Forms.BindingSource bindingSource1;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxAliasColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxFilenameColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxArgumentsColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn uxModesColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn uxNotesColumn;
	}
}
