using System;
using System.Collections.Generic;

namespace ToolBox
{

    public class EventManager
    {
        protected static Dictionary<EventsNames, List<System.Action<object>>> _events;     //список для подписок

        private static void Create(EventsNames eventName)
        {
            //NullReferenceException handling

            if (_events == null)
                _events = new Dictionary<EventsNames, List<Action<object>>>();

            if (!_events.ContainsKey(eventName))
            {
                _events.Add(eventName, new List<Action<object>>());
            }


        }



        public static void Subscribe(EventsNames eventName, Action<object> action)
        {
            Create(eventName);
            _events[eventName].Add(action);

        }

        public static void Unsubscribe(EventsNames eventName, Action<object> action)
        {
            Create(eventName);
            if (_events[eventName].Contains(action))
            {
                _events[eventName].Remove(action);
            }
            if (_events[eventName].Count < 1)
                _events.Remove(eventName);

        }

        public static void Publish(EventsNames eventName, IEventSignal signal)
        {
            Create(eventName);
            foreach (var action in _events[eventName])
            {
                action(signal);
            }


        }

        public static void Clear()
        {
            //change scene -> delete all subscribes

            if (_events != null) _events.Clear();
        }


    }

    public enum EventsNames
    {
        Empty,      //always first!!!
    };

    public interface IEventSignal
    {
    }


}