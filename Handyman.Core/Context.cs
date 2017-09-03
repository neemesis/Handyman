using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Handyman.Core.Forms;
using Handyman.Framework;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;
using Handyman.Core.Helpers;
using Handyman.Framework.Parsers;

namespace Handyman.Core {
    public class Context : IDisposable {
        #region Properties
        public List<Commands> Handymans { get; set; }
        public List<IMaster> Tools { get; set; }
        private static IParse Parser { get; set; }
        private System.ComponentModel.IContainer __components;
        #endregion

        private Context() {
            #region Register app at windows startup
            if (Properties.Settings.Default.RunAtWindowsStart) {
                Utilities.RunOnStart("Handyman", Application.ExecutablePath);
            } else {
                Utilities.RemoveRunOnStart("Handyman");
            }
            #endregion

            Parser = new DefaultParser();
            Handymans = HandymansManager.Load();
            Tools = PluginManager.LoadPlugins(out __components);
        }

        #region Public Methods

        /// <summary>
        /// Gets the auto complete source.
        /// </summary>
        /// <value>The auto complete source.</value>
        public string[] AutoCompleteSource {
            get {
                var app = AppForms.GetAppForms();

                app.AddRange(Handymans.Select(word => word.Alias));
                app.AddRange(Tools.Select(tool => tool.Alias));

                return app.ToArray();
            }
        }

        public void Start(string alias) {
            // Save to history
            History.Add(alias);

            // Check for app commands
            if (AppForms.HandleForm(alias))
                return;

            // Use HelpUrl to open online help
            if (Executor.ExecuteHelp(Tools, alias))
                return;

            // Check for list packages or install
            if (PluginManager.Handle(alias))
                return;

            // Try to find tool to execute
            if (Executor.ExecuteTool(Tools, alias, Parser, SetError))
                return;

            // Try to find Handyman to execute
            if (Executor.ExecuteHandyman(Handymans, alias, SetError))
                return;
            
            // Fallback execute cmd command
            Executor.ExecuteFallback(alias);
        }

        private static void SetError(Exception e = null) {
            Launcher.Current.ShowData("error :(");
        }
        #endregion

        #region Singleton
        private static volatile Context _singleton;
        private static readonly object SyncRoot = new object();

        public static Context Current {
            get {
                if (_singleton == null) {
                    lock (SyncRoot) {
                        if (_singleton == null) {
                            _singleton = new Context();
                        }
                    }
                }

                return _singleton;
            }
        }
        #endregion

        public static void AddActiveApplicationHandyman(string appExeName, string appExePath) {
            var word = new Commands {
                Alias = appExeName,
                FileName = appExePath
            };

            AppForms.ShowHandymanForm(word);
        }

        #region IDisposable Members

        void IDisposable.Dispose() {
            __components?.Dispose();
        }

        #endregion
    }
}
