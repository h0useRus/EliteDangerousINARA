using System;
using NSW.EliteDangerous.INARA.Commands;

namespace NSW.EliteDangerous.INARA
{
    public interface IEliteDangerousINARA : IDisposable
    {
        bool IsApiAttached { get; }
        void SetCommander(string commander, string frontierId = null);
        InaraRequest AddCommand<TCommand>(TCommand command) where TCommand : Command;
        InaraRequest StartRequest();
    }
}