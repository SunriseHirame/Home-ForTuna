using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hirame.Minerva
{
    [CreateAssetMenu (menuName = "Hirame/Game Events/Game Event")]
    public class GameEvent : ScriptableObject
    {
        [SerializeField] private List<GameEventListener> dynamicListeners = new List<GameEventListener> ();

        [SerializeField] private bool enableStaticEvent;
        [SerializeField] private UnityEvent staticEvent;

        public void RaiseEvent ()
        {
            for (var i = dynamicListeners.Count - 1; i >= 0; i--)
            {
                dynamicListeners[i].OnEventRaised ();
            }

            if (enableStaticEvent)
            {
                staticEvent.Invoke ();
            }
        }
        
        public bool TryAddListener (GameEventListener listener)
        {
            if (dynamicListeners.Contains (listener))
                return false;

            dynamicListeners.Add (listener);
            return true;
        }

        public void AddListener (GameEventListener listener)
        {
            dynamicListeners.Add (listener);
        }

        public void AddUniqueListener (GameEventListener listener)
        {
            if (!dynamicListeners.Contains (listener))
                dynamicListeners.Add (listener);
        }

        public void RemoveListener (GameEventListener listener)
        {
            dynamicListeners.Remove (listener);
        }

        public void RemoveAllListeners ()
        {
            dynamicListeners.Clear ();
        }
    }
}