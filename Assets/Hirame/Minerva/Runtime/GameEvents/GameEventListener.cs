using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Hirame.Minerva
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent ListenedEvent;
        public UnityEvent Event;

        public GameEventListener ()
        {
            // TODO:
            // Add logic to enable true lifetime event binding.
        }

        internal void OnEventRaised ()
        {
            Event.Invoke ();
        }

        private void OnEnable ()
        {
            ListenedEvent.AddListener (this);
        }

        private void OnDisable ()
        {
            ListenedEvent.RemoveListener (this);
        }
    }
}