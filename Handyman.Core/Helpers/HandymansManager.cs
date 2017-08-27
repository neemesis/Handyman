using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Handyman.Framework.Entities;

namespace Handyman.Core.Helpers {
    public static class HandymansManager {
        private static readonly string MWordsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".Handymans";

        public static List<Commands> Load() {
            if (File.Exists(MWordsPath)) {
                return LoadInternal();
            } else {
                var res = Defaults.Default();
                Save(res);
                return res;
            }
        }

        private static List<Commands> LoadInternal() {
            var serializer = new XmlSerializer(typeof(List<Commands>));
            var reader = File.OpenText(MWordsPath);
            var result = (List<Commands>)serializer.Deserialize(reader);
            reader.Close();
            return result;
        }

        public static void Save(List<Commands> comm) {
            var ser = new XmlSerializer(typeof(List<Commands>));
            var sw = new StreamWriter(MWordsPath);
            ser.Serialize(sw, comm);
            sw.Close();
        }
    }
}
