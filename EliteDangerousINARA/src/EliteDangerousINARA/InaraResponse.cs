using System.Collections.Generic;
using NSW.EliteDangerous.INARA.Events;

namespace NSW.EliteDangerous.INARA
{
    public class InaraResponse
    {
        public ResponseStatus Status { get; internal set; }
        public string StatusText { get; internal set; }
        public InaraUser User { get; internal set; }
        public List<EventResult> Events { get; } = new List<EventResult>();
    }
}