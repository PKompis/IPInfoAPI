using System;

namespace IPInfo.Resources
{
    public class TrackResponseIdentifierResource

    {
        public TrackResponseIdentifierResource(Guid trackProgress)
        {
            TrackProgress = trackProgress;
        }

        public Guid TrackProgress { get; set; }
    }
}
