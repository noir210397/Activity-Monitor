using System.Security.Cryptography.X509Certificates;

namespace Activity_Monitor.Events
{
    public enum EventLevel
    {
        Info,
        Warning,
        Error,
        Critical
    }
    public enum WorkEvent
    {
        Started,Completed, Failed, Cancelled, Paused, Resumed
    }
    public abstract class Event
    {
        public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    }
    
    public class ClientEvent : Event
    {
        public EventLevel Level { get; init; } =  EventLevel.Info;
        public long ClientCount { get; init; }
        public ClientEvent( long clientCount)
        {      
            ClientCount = clientCount;
        }
    }
    public class WorkEventRecord : Event
    {
        public WorkEvent EventType { get; init; }
        public EventLevel Level { get; init; }

        public WorkEventRecord(WorkEvent eventType, EventLevel level)
        {
            EventType = eventType;
            Level = level;
        }
    }
    
}
