using System;
using System.Diagnostics;

namespace Slave.Framework.Entities
{
	/// <summary>
	/// A magic word is an alias, a keyword, to launch one or more program, url, or files.
	/// </summary>
	[Serializable()]
	public class Commands : System.ComponentModel.INotifyPropertyChanged
	{
		public Commands()
		{
			_mStartUpMode = ProcessWindowStyle.Normal;
		}

		#region Public string Alias

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _mAlias;

		/// <summary>
		/// Gets or sets the Alias.
		/// </summary>
		/// <value>The Alias.</value>
		[System.ComponentModel.Bindable(true)]
		public string Alias
		{
			[DebuggerStepThrough]
			get { return _mAlias; }
			[DebuggerStepThrough]
			set
			{
				if (_mAlias != value)
				{
					_mAlias = value;
					OnPropertyChanged("Alias");
				}
			}
		}
		#endregion

		#region Public string FileName

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _mFileName;

		/// <summary>
		/// Gets or sets the FileName.
		/// </summary>
		/// <value>The FileName.</value>
		[System.ComponentModel.Bindable(true)]
		public string FileName
		{
			[DebuggerStepThrough]
			get { return _mFileName; }
			[DebuggerStepThrough]
			set
			{
				if (_mFileName != value)
				{
					_mFileName = value;
					OnPropertyChanged("FileName");
				}
			}
		}
		#endregion

		#region Public System.Diagnostics.ProcessWindowStyle StartUpMode

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private ProcessWindowStyle _mStartUpMode;

		/// <summary>
		/// Gets or sets the StartUpMode.
		/// </summary>
		/// <value>The StartUpMode.</value>
		[System.ComponentModel.Bindable(true)]
		public ProcessWindowStyle StartUpMode
		{
			[DebuggerStepThrough]
			get { return _mStartUpMode; }
			[DebuggerStepThrough]
			set
			{
				if (_mStartUpMode != value)
				{
					_mStartUpMode = value;
					OnPropertyChanged("StartUpMode");
				}
			}
		}
		#endregion

		#region Public string StartUpPath

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _mWorkingDirectory;

		/// <summary>
		/// Gets or sets the StartUpPath.
		/// </summary>
		/// <value>The StartUpPath.</value>
		[System.ComponentModel.Bindable(true)]
		public string WorkingDirectory
		{
			[DebuggerStepThrough]
			get { return _mWorkingDirectory; }
			[DebuggerStepThrough]
			set
			{
				if (_mWorkingDirectory != value)
				{
					_mWorkingDirectory = value;
					OnPropertyChanged("WorkingDirectory");
				}
			}
		}
		#endregion

		#region Public string Parameters

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _mArguments;

		/// <summary>
		/// Gets or sets the Parameters.
		/// </summary>
		/// <value>The Parameters.</value>
		[System.ComponentModel.Bindable(true)]
		public string Arguments
		{
			[DebuggerStepThrough]
			get { return _mArguments; }
			[DebuggerStepThrough]
			set
			{
				if (_mArguments != value)
				{
					_mArguments = value;
					OnPropertyChanged("Arguments");
				}
			}
		}
		#endregion

		#region Public string Notes

		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string _mNotes;

		/// <summary>
		/// Gets or sets the Notes.
		/// </summary>
		/// <value>The Notes.</value>
		[System.ComponentModel.Bindable(true)]
		public string Notes
		{
			[DebuggerStepThrough]
			get { return _mNotes; }
			[DebuggerStepThrough]
			set
			{
				if (_mNotes != value)
				{
					_mNotes = value;
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
