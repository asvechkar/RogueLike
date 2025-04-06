using System;
using System.Collections.Generic;

namespace RogueLike.Scripts.Events
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> Subscriptions = new Dictionary<Type, List<Delegate>>();

        public static void Subscribe<T>(Action<T> listener)
        {
            var eventType = typeof(T);
            if (!Subscriptions.ContainsKey(eventType))
            {
                Subscriptions[eventType] = new List<Delegate>();
            }

            Subscriptions[eventType].Add(listener);
        }

        public static void Unsubscribe<T>(Action<T> listener)
        {
            var eventType = typeof(T);
            if (Subscriptions.TryGetValue(eventType, out var listeners))
            {
                listeners.Remove(listener);
            }
        }

        public static void Invoke<T>(T eventData)
        {
            var eventType = typeof(T);
            if (!Subscriptions.TryGetValue(eventType, out var listeners)) return;
            
            foreach (var del in listeners)
            {
                // Приводим к Action<T> и вызываем
                (del as Action<T>)?.Invoke(eventData);
            }
        }
    }
}