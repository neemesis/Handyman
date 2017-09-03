using System;
using System.Collections.Generic;
using System.ComponentModel;
using Handyman.Framework.Entities;

namespace Handyman.Framework.Interfaces {
    public interface IMaster {
        /// <summary>
        /// Name of the plugin
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Description of the plugin
        /// </summary>
        string Description { get; }
        /// <summary>
        /// Author of the plugin
        /// </summary>
        string Author { get; }
        /// <summary>
        /// Version of the plugin (ex. 1.0.0.0). 
        /// </summary>
        string Version { get; }
        /// <summary>
        /// Help url of the plugin, usally url to git README.MD file
        /// </summary>
        string HelpUrl { get; }
        /// <summary>
        /// Shortcut for executing tool without writing to launcher.
        /// </summary>
        [Editor(@"System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        System.Windows.Forms.Shortcut HotKey { get; set; }
        /// <summary>
        /// Alias of the tool, for starting it from the launcher
        /// </summary>
        string Alias { get; set; }
        /// <summary>
        /// Custom parser, if it's null the default parser is used
        /// </summary>
        IParse Parser { get; set; }

        List<string> Suggestions { get; set; }

        /// <summary>
        /// Called when application is started. Load your data here
        /// </summary>
        void Initialize();
        /// <summary>
        /// Called when your alias was entered in the launcher
        /// </summary>
        /// <param name="args">List of arguments returned from the parser.</param>
        /// <param name="display">Function used to display short data on the launcher. Use it like this display("Some string Data", DisplayData)</param>
        void Execute(string[] args, Action<string, DisplayData> display);
    }
}
