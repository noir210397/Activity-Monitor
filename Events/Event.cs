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
        public string Level { get; init; } =  EventLevel.Info.ToString();
        public long ClientCount { get; init; }
        public ClientEvent( long clientCount)
        {      
            ClientCount = clientCount;
        }
    }
    public class WorkEventRecord : Event
    {
        public string EventType { get; init; }
        public string Level { get; init; }

        public WorkEventRecord(string eventType, string level)
        {
            EventType = eventType;
            Level = level;
        }
    }
    
}
