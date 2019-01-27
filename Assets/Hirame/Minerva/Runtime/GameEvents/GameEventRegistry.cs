using System.Collections.Generic;
using UnityEngine;

namespace Hirame.Minerva.GameEvents
{
    public static class GameEventRegistry
    {      
        private static readonly Dictionary<string, GameEvent> EventLibrary = new Dictionary<string, GameEvent> ();

        public static bool TryGetEventWithName (string name, out GameEvent gameEvent)
        {
            return EventLibrary.TryGetValue (name, out gameEvent);
        }
        
        private static void RegisterEvent (GameEvent gEvent)
        {
            var name = gEvent.name;
            if (EventLibrary.ContainsKey (name))
                return;           
            EventLibrary.Add (name, gEvent);
        }
        
        [RuntimeInitializeOnLoadMethod]
        private static void Init ()
        {           
            // TODO:
            // This could be optimized.
            var gameEvents = Resources.LoadAll<GameEvent> (string.Empty);
            foreach (var e in gameEvents)
            {
                RegisterEvent (e);
            }          
        }
    }
}
