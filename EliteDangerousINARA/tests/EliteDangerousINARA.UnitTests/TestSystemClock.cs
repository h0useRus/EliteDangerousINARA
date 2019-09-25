using System;

namespace NSW.EliteDangerous.INARA
{
    internal class TestSystemClock : ISystemClock
    {
        public DateTime Now => new DateTime(2019, 9, 2, 17, 0, 0, DateTimeKind.Utc);
    }
}