using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Slave.Core.Forms;
using Slave.Framework;
using Slave.Framework.Entities;
using Slave.Framework.Interfaces;
using Slave.Core.Helpers;
using Slave.Framework.Parsers;

namespace Slave.Core {
    public class Context : IDisposable {
        #region Properties
        public List<Commands> Slaves { get; set; }
        public List<IMaster> Tools { get; set; }
        private static IParse Parser { get; set; }
        private System.ComponentModel.IContainer _components;
        #endregion

        private Context() {
            #region Register app at windows startup
            if (Properties.Settings.Default.RunAtWindowsStart) {
                Utilities.RunOnStart("Slaver", Application.ExecutablePath);
            } else {
                Utilities.RemoveRunOnStart("Slaver");
            }
            #endregion

            Parser = new DefaultParser();
            Slaves = SlavesManager.Load();
            Tools = PluginManager.LoadPlugins(out _components);
        }

        #region public methods

        /// <summary>
        /// Gets the auto complete source.
        /// </summary>
        /// <value>The auto complete source.</value>
        public string[] AutoCompleteSource {
            get {
                var app = AppForms.GetAppForms();

                app.AddRange(Slaves.Select(word => word.Alias));
                app.AddRange(Tools.Select(tool => tool.Alias));

                return app.ToArray();
            }
        }

        public void Start(string alias) {
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

            // Try to find slave to execute
            if (Executor.ExecuteSlave(Slaves, alias, SetError))
                return;
            
            // Fallback execute cmd command
            Executor.ExecuteFallback(alias);
        }

        private static void SetError(Exception e = null) {
            Launcher.Current.ChangeLauncherText("error :(");
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

        public void AddActiveApplicationSlave(string appExeName, string appExePath) {
            var word = new Commands {
                Alias = appExeName,
                FileName = appExePath
            };

            AppForms.ShowSlaveForm(word);
        }

        #region IDisposable Members

        void IDisposable.Dispose() {
            _components?.Dispose();
        }

        #endregion
    }
}
