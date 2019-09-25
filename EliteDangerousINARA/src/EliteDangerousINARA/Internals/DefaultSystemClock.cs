using System;

namespace NSW.EliteDangerous.INARA
{
    public class DefaultSystemClock : ISystemClock
    {
        public DateTime Now => DateTime.UtcNow;
    }
}