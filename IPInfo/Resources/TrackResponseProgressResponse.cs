using System;

namespace IPInfo.Resources
{
    public class TrackResponseProgressResponse
    {
        public TrackResponseProgressResponse(string progress)
        {
            Progress = progress;
        }

        public string Progress { get; set; }
    }
}
