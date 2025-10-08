using System.Collections.Generic;
using UnityEngine;

namespace VadyaRus.Events {
    public static class EventBus<T> where T : IEvent {
        static readonly HashSet<IEventBinding<T>> bindings;

        static EventBus() {
            bindings = new HashSet<IEventBinding<T>>();
        }

        public static void Register(EventBinding<T> binding) {
            // Add a null check to prevent invalid bindings from being added.
            if (binding == null) {
                Debug.LogWarning($"Attempted to register a null binding for event type {typeof(T).Name}.");
                return;
            }
            bindings.Add(binding);
        }
        public static void Unregister(EventBinding<T> binding) {
            if (binding == null) return;
            bindings.Remove(binding);
        }

        public static void Raise(T @event) {
            // Creating a snapshot to prevent issues if a binding is
            // registered/unregistered during the event raise.
            var snapshot = new HashSet<IEventBinding<T>>(bindings);

            foreach (var binding in snapshot) {
                // Check if the binding still exists in the original set,
                // in case it was removed during the loop.
                if (bindings.Contains(binding)) {
                    binding.OnEvent?.Invoke(@event);
                    binding.OnEventNoArgs?.Invoke();
                }
            }
        }

        static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings.");
            bindings.Clear();
        }
    }
}