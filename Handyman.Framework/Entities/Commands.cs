using System;
using System.Diagnostics;

namespace Handyman.Framework.Entities {
    /// <summary>
    /// A magic word is an alias, a keyword, to launch one or more program, url, or files.
    /// </summary>
    [Serializable]
    public class Commands : System.ComponentModel.INotifyPropertyChanged {
        public Commands() {
            _startUpMode = ProcessWindowStyle.Normal;
        }

        #region Public string Alias

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _alias;

        /// <summary>
        /// Gets or sets the Alias.
        /// </summary>
        /// <value>The Alias.</value>
        [System.ComponentModel.Bindable(true)]
        public string Alias {
            [DebuggerStepThrough]
            get { return _alias; }
            [DebuggerStepThrough]
            set {
                if (_alias != value) {
                    _alias = value;
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
        public string FileName {
            [DebuggerStepThrough]
            get { return _mFileName; }
            [DebuggerStepThrough]
            set {
                if (_mFileName != value) {
                    _mFileName = value;
                    OnPropertyChanged("FileName");
                }
            }
        }
        #endregion

        #region Public System.Diagnostics.ProcessWindowStyle StartUpMode

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ProcessWindowStyle _startUpMode;

        /// <summary>
        /// Gets or sets the StartUpMode.
        /// </summary>
        /// <value>The StartUpMode.</value>
        [System.ComponentModel.Bindable(true)]
        public ProcessWindowStyle StartUpMode {
            [DebuggerStepThrough]
            get { return _startUpMode; }
            [DebuggerStepThrough]
            set {
                if (_startUpMode != value) {
                    _startUpMode = value;
                    OnPropertyChanged("StartUpMode");
                }
            }
        }
        #endregion

        #region Public string StartUpPath

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _workingDirectory;

        /// <summary>
        /// Gets or sets the StartUpPath.
        /// </summary>
        /// <value>The StartUpPath.</value>
        [System.ComponentModel.Bindable(true)]
        public string WorkingDirectory {
            [DebuggerStepThrough]
            get { return _workingDirectory; }
            [DebuggerStepThrough]
            set {
                if (_workingDirectory != value) {
                    _workingDirectory = value;
                    OnPropertyChanged("WorkingDirectory");
                }
            }
        }
        #endregion

        #region Public string Parameters

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _arguments;

        /// <summary>
        /// Gets or sets the Parameters.
        /// </summary>
        /// <value>The Parameters.</value>
        [System.ComponentModel.Bindable(true)]
        public string Arguments {
            [DebuggerStepThrough]
            get { return _arguments; }
            [DebuggerStepThrough]
            set {
                if (_arguments != value) {
                    _arguments = value;
                    OnPropertyChanged("Arguments");
                }
            }
        }
        #endregion

        #region Public string Notes

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string _notes;

        /// <summary>
        /// Gets or sets the Notes.
        /// </summary>
        /// <value>The Notes.</value>
        [System.ComponentModel.Bindable(true)]
        public string Notes {
            [DebuggerStepThrough]
            get { return _notes; }
            [DebuggerStepThrough]
            set {
                if (_notes != value) {
                    _notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
