using System;
using System.Collections.Generic;
using UnityEngine;

namespace EventSystem
{
    public interface IEvent { }

    public class EventManager : MonoBehaviour {

        private Dictionary<Type, Action<IEvent>> eventDictionary;
        
        public static EventManager Instance { get; private set; }
        
        private void Awake() 
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
                Instance.Setup();
            } 
        }

        private void Setup()
        {
            DontDestroyOnLoad(gameObject);
            eventDictionary ??= new Dictionary<Type, Action<IEvent>>();
        }

        public static void Subscribe(Type eventType, Action<IEvent> listener)
        {
            if (Instance.eventDictionary.TryGetValue(eventType, out var thisEvent))
            {
                thisEvent += listener;
                Instance.eventDictionary[eventType] = thisEvent;
            } 
            else
            {
                thisEvent += listener;
                Instance.eventDictionary.Add(eventType, thisEvent);
            }
        }

        public static void Unsubscribe(Type eventType, Action<IEvent> listener)
        {
            if (Instance == null) return;
            if (!Instance.eventDictionary.TryGetValue(eventType, out var thisEvent)) return;
            
            thisEvent -= listener;
            Instance.eventDictionary[eventType] = thisEvent;
        }

        public static void Trigger(IEvent eventInstance)
        {
            if (Instance.eventDictionary.TryGetValue(eventInstance.GetType(), out var thisEvent))
            {
                thisEvent.Invoke(eventInstance);
            }
        }
    }
}