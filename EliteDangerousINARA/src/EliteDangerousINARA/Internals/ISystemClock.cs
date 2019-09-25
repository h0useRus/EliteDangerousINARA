using System;

namespace NSW.EliteDangerous.INARA
{
    internal interface ISystemClock
    {
        DateTime Now { get; }
    }
}