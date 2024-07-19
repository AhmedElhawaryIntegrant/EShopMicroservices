

using System.Reflection;

namespace BuildingBlocks.Messaging.Events
{
    public record IntegrationEvent
    {
        public Guid Id => Guid.NewGuid();

        public DateTime CreationDate => DateTime.UtcNow;

        public string EventType => GetType().AssemblyQualifiedName;
    }
}
