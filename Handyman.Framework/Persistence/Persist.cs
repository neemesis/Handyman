using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Handyman.Framework.Persistence {
    public static class Persist {
        private static string _oldSuffix = ".Handymanconfig";
        private static string _suffix = ".hmcfg";
        private static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";

        /// <summary>
        /// Save data to xml serializable document
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="alias">Alias</param>
        /// <returns>Is it success?</returns>
        public static bool Save<T>(T data, string alias) {
            try {
                var path = Path + alias + _suffix;
                var ser = new XmlSerializer(typeof(T));
                var sw = new StreamWriter(path);
                ser.Serialize(sw, data);
                sw.Close();
                return true;
            } catch (Exception e) {
                return false;
            }

        }

        /// <summary>
        /// Load data from list.
        /// </summary>
        /// <typeparam name="T">Type to which to convert</typeparam>
        /// <param name="alias">Alias</param>
        /// <returns>List of objects</returns>
        public static T Load<T>(string alias) {
            var path = Path + alias + _oldSuffix;
            if (File.Exists(path)) {
                var res = LoadInternal<T>(alias, path);
                Save(res, alias);
                File.Delete(path);
                return res;
            }
            return LoadInternal<T>(alias, Path + alias + _suffix);
        } 
        
        private static T LoadInternal<T>(string alias, string path) {
            try {
                if (File.Exists(path)) {
                    var serializer = new XmlSerializer(typeof(T));
                    var reader = File.OpenText(path);
                    var res = (T)serializer.Deserialize(reader);
                    reader.Close();
                    return res;
                } else {
                    var res = default(T);
                    Save(res, alias);
                    return res;
                }
            } catch (Exception e) {
                return default(T);
            }
        }

        /// <summary>
        /// Delete saved list.
        /// </summary>
        /// <param name="alias">Name of the list.</param>
        /// <returns>Is it success?</returns>
        public static bool Delete(string alias) {
            try {
                var path = Path + alias + ".Handymanconfig";
                if (File.Exists(path)) {
                    File.Delete(path);
                    return true;
                }
                return false;
            } catch { return false; }
        }
    }
}
