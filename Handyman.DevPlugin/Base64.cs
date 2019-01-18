using System;
using System.IO;

namespace Handyman.DevPlugin {
    public static class Base64 {
        public static void FromFile(string path) {
            var bytes = File.ReadAllBytes(path);
            var file = Convert.ToBase64String(bytes);
            File.WriteAllText(path + ".base64", file);
        }
    }
}
