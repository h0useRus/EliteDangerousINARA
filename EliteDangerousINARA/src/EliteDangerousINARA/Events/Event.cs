using System.Collections.Generic;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA.Events
{
    public class EventResult
    {
        public ResponseStatus Status { get; internal set; }
        public string StatusText { get; internal set; }
        public string Name { get; internal set; }
    }

    public class EventResult<TData> : EventResult
    {
        public TData Data { get; internal set; }
    }

}