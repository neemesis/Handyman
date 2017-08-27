using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Handyman.Core.Entities
{
	/// <summary>
	/// A magic word is an alias, a keyword, to launch one or more program, url, or files.
	/// </summary>
	[Serializable()]
	public class Handyman : System.ComponentModel.INotifyPropertyChanged
	{
		public Handyman()
		{
			m_StartUpMode = ProcessWindowStyle.Normal;
		}

		#region Public string Alias

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private string m_Alias;

		/// <summary>
		/// Gets or sets the Alias.
		/// </summary>
		/// <value>The Alias.</value>
		[System.ComponentModel.Bindable(true)]
		public string Alias
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_Alias; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_Alias != value)
				{
					this.m_Alias = value;
					OnPropertyChanged("Alias");
				}
			}
		}
		#endregion

		#region Public string FileName

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private string m_FileName;

		/// <summary>
		/// Gets or sets the FileName.
		/// </summary>
		/// <value>The FileName.</value>
		[System.ComponentModel.Bindable(true)]
		public string FileName
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_FileName; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_FileName != value)
				{
					this.m_FileName = value;
					OnPropertyChanged("FileName");
				}
			}
		}
		#endregion

		#region Public System.Diagnostics.ProcessWindowStyle StartUpMode

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private System.Diagnostics.ProcessWindowStyle m_StartUpMode;

		/// <summary>
		/// Gets or sets the StartUpMode.
		/// </summary>
		/// <value>The StartUpMode.</value>
		[System.ComponentModel.Bindable(true)]
		public System.Diagnostics.ProcessWindowStyle StartUpMode
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_StartUpMode; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_StartUpMode != value)
				{
					this.m_StartUpMode = value;
					OnPropertyChanged("StartUpMode");
				}
			}
		}
		#endregion

		#region Public string StartUpPath

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private string m_WorkingDirectory;

		/// <summary>
		/// Gets or sets the StartUpPath.
		/// </summary>
		/// <value>The StartUpPath.</value>
		[System.ComponentModel.Bindable(true)]
		public string WorkingDirectory
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_WorkingDirectory; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_WorkingDirectory != value)
				{
					this.m_WorkingDirectory = value;
					OnPropertyChanged("WorkingDirectory");
				}
			}
		}
		#endregion

		#region Public string Parameters

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private string m_Arguments;

		/// <summary>
		/// Gets or sets the Parameters.
		/// </summary>
		/// <value>The Parameters.</value>
		[System.ComponentModel.Bindable(true)]
		public string Arguments
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_Arguments; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_Arguments != value)
				{
					this.m_Arguments = value;
					OnPropertyChanged("Arguments");
				}
			}
		}
		#endregion

		#region Public string Notes

		[System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
		private string m_Notes;

		/// <summary>
		/// Gets or sets the Notes.
		/// </summary>
		/// <value>The Notes.</value>
		[System.ComponentModel.Bindable(true)]
		public string Notes
		{
			[System.Diagnostics.DebuggerStepThrough]
			get { return this.m_Notes; }
			[System.Diagnostics.DebuggerStepThrough]
			set
			{
				if (this.m_Notes != value)
				{
					this.m_Notes = value;
					OnPropertyChanged("Notes");
				}
			}
		}
		#endregion
				
		#region INotifyPropertyChanged Members

		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
