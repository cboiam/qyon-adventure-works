using MediatR;
using QyonAdventureWorks.Core.Entities;

namespace QyonAdventureWorks.Core.Commands.CircuitCommands
{
    public class AddCircuitCommand : IRequest<Circuit>
    {
        public string Description { get; set; }
    }
}
