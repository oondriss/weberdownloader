using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Infrastructure
{
    [EventSource(Name = "WeberReader")]
    public class WeberEventSource : EventSource
    {
        public static readonly WeberEventSource Log = new WeberEventSource();
        private static readonly bool EventSourceEnabled = false;
        class Keywords
        {
            public const EventKeywords Service = (EventKeywords) 1;
            public const EventKeywords Communication = (EventKeywords) 2;
            public const EventKeywords Output = (EventKeywords) 4;
        }

        class Tasks
        {
            public const EventTask Execution = (EventTask) 1;
        }

        [Event(1, Message = "Service starting", Level = EventLevel.Informational, Keywords = Keywords.Service, Task = Tasks.Execution)]
        public void StartService(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(1, message); 
            }
        }

        [Event(2, Message = "Service stopping", Level = EventLevel.Informational, Keywords = Keywords.Service, Task = Tasks.Execution)]
        public void StopService(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(2, message); 
            }
        }

        [Event(3, Message = "Communication starting", Level = EventLevel.Informational, Keywords = Keywords.Communication, Task = Tasks.Execution)]
        public void StartCommunication(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(3, message); 
            }
        }

        [Event(4, Message = "Communication stopping", Level = EventLevel.Informational, Keywords = Keywords.Communication, Task = Tasks.Execution)]
        public void StopCommunication(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(4, message); 
            }
        }

        [Event(5, Message = "Output starting", Level = EventLevel.Informational, Keywords = Keywords.Output, Task = Tasks.Execution)]
        public void StartOutput(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(5, message); 
            }
        }

        [Event(6, Message = "Output stopping", Level = EventLevel.Informational, Keywords = Keywords.Output, Task = Tasks.Execution)]
        public void StopOutput(string message)
        {
            if (EventSourceEnabled)
            {
                WriteEvent(6, message); 
            }
        }
    }
}