using System;
using System.Collections.Generic;
using System.Reflection;
using EventBus.Boot;
using EventBus.Interfaces;
using UnityEditor;
using UnityEngine;

namespace EventBus
{
    public static class EventBusUtil
    {
        public static IReadOnlyList<Type> EventTypes { get; set; }
        public static IReadOnlyList<Type> EventBusTypes { get; set; }
        
        #if UNITY_EDITOR

        public static PlayModeStateChange PlayModeState { get; set; }

        [InitializeOnLoadMethod]
        public static void InitializeEditor()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private static void OnPlayModeStateChanged(PlayModeStateChange state)
        {
            PlayModeState = state;

            if (state == PlayModeStateChange.ExitingPlayMode)
            {
                ClearAllBuses();
            }
        }
        
        #endif

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void Initialize()
        {
            EventTypes = PredefinedAssemblyUtil.GetTypes(typeof(IEvent));
            EventBusTypes = InitializeAllBuses();
        }

        private static void ClearAllBuses()
        {
            foreach (Type busType in EventBusTypes)
            {
                MethodInfo clearMethod = busType.GetMethod("Clear", BindingFlags.Static | BindingFlags.NonPublic);
                
                if (clearMethod != null)
                    clearMethod.Invoke(null, null);
            }

            Debug.Log("All buses clear");
        }

        private static List<Type> InitializeAllBuses()
        {
            List<Type> eventBusTypes = new();

            Type type = typeof(EventBus<>);

            foreach (Type eventType in EventTypes)
            {
                Type busType = type.MakeGenericType(eventType);
                eventBusTypes.Add(busType);
                Debug.Log($"Initialized {eventType.Name}");
            }

            return eventBusTypes;
        }
    }
}