using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Hirame.Minerva.Editor
{
    /// <summary>
    /// A super Minimal base drawer that hides the annoying script field.
    /// </summary>
    [CustomEditor (typeof (UnityEngine.Object), true)]
    public class MinervaEditorBase : UnityEditor.Editor
    {
        private readonly HashSet<string> dontDrawSet = new HashSet<string> {"m_Script"};
        private string[] dontDraw = {"m_Script"};

        public override void OnInspectorGUI ()
        {
            using (var changeScope = new EditorGUI.ChangeCheckScope ())
            {
                DrawPropertiesExcluding (serializedObject, dontDraw);

                if (changeScope.changed)
                    serializedObject.ApplyModifiedProperties ();
            }
        }

        protected void DontDraw (string propertyName)
        {
            dontDrawSet.Add (propertyName);
            dontDraw = dontDrawSet.ToArray ();
        }
    }
}