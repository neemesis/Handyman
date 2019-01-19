using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman.Core.Extensions {
    public static class Extensions {
        public static bool OpenHelp(this IMaster proc) {
            if (proc == null)
                return false;
            Process.Start(proc.HelpUrl);
            return true;
        }
    }
}
