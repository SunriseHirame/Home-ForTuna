using UnityEngine;
using UnityEditor;

namespace Hirame.Minerva.Editor
{
    [CustomEditor (typeof (GlobalValueBase), true)]
    public class GlobalValueEditor : MinervaEditorBase
    {
        private GlobalValueBase global;
        
        protected virtual void OnEnable ()
        {
            global = target as GlobalValueBase;
        }

        public override void OnInspectorGUI ()
        {
            base.OnInspectorGUI ();
            if (GUILayout.Button ("Reset"))
            {
                using (var changeScope = new EditorGUI.ChangeCheckScope ())
                {
                    global.Reset ();
                    serializedObject.Update ();
                    EditorUtility.SetDirty (this);
                }
                
            }
        }
    }
}
