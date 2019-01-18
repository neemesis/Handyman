using System;

namespace Handyman.TrackPlugin {
    public class TrackData {
        public Project Project { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}
