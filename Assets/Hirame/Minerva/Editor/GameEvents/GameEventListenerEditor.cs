using UnityEditor;

namespace Hirame.Minerva.Editor
{
    [CustomEditor (typeof (GameEventListener))]
    public class GameEventListenerEditor : MinervaEditorBase
    {
        private GameEventListener listener;
        
        private void OnEnable ()
        {
            listener = target as GameEventListener;
        }
        
    }
}
