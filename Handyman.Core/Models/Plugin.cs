﻿namespace Handyman.Core.Models {
    public class Plugin {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public bool HasConfig { get; set; }
        public string URL { get; set; }
    }
}
