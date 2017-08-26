using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Slave.Framework.Persistence {
    public static class Persist {
        private static readonly string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";

        /// <summary>
        /// Save data to xml serializable document
        /// </summary>
        /// <typeparam name="T">Type of objects</typeparam>
        /// <param name="data">Data to save</param>
        /// <param name="alias">Alias</param>
        /// <returns>Is it success?</returns>
        public static bool Save<T>(IList<T> data, string alias) {
            try {
                var path = Path + alias + ".slaveconfig";
                var ser = new XmlSerializer(typeof(List<T>));
                var sw = new StreamWriter(path);
                ser.Serialize(sw, data);
                sw.Close();
                return true;
            }
            catch (Exception e) {
                return false;
            }
            
        }

        /// <summary>
        /// Load data from list.
        /// </summary>
        /// <typeparam name="T">Type to which to convert</typeparam>
        /// <param name="alias">Alias</param>
        /// <returns>List of objects</returns>
        public static List<T> Load<T>(string alias) {
            try {
                var path = Path + alias + ".slaveconfig";
                if (File.Exists(path)) {
                    var serializer = new XmlSerializer(typeof(List<T>));
                    var reader = File.OpenText(path);
                    var res = (List<T>) serializer.Deserialize(reader);
                    reader.Close();
                    return res;
                }
                else {
                    var res = new List<T>();
                    Save(res, alias);
                    return res;
                }
            }
            catch {
                return new List<T>();
            }
        }

        /// <summary>
        /// Delete saved list.
        /// </summary>
        /// <param name="alias">Name of the list.</param>
        /// <returns>Is it success?</returns>
        public static bool Delete(string alias) {
            try {
                var path = Path + alias + ".slaveconfig";
                if (File.Exists(path)) {
                    File.Delete(path);
                    return true;
                }
                return false;
            } catch {return false;}
        }
    }
}
