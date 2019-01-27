using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Hirame.HighScore
{
    [CustomEditor (typeof (HighScoreDisplay))]
    public class HighScoreEditor : Editor
    {
        private HighScoreDisplay highScoreDisplay;

        private void OnEnable ()
        {
            highScoreDisplay = target as HighScoreDisplay;
        }

        public override void OnInspectorGUI ()
        {
            if (GUILayout.Button ("Open web view"))
            {
                Application.OpenURL (HighScoreManager.HighScoreUrl);
            }

            EditorGUILayout.Space ();
            
            highScoreDisplay.Username = EditorGUILayout.TextField ("Username", highScoreDisplay.Username);
            highScoreDisplay.Score = EditorGUILayout.TextField ("Score", highScoreDisplay.Score);
            
            if (GUILayout.Button ("Add Score"))
                HighScoreManager.UploadScore (highScoreDisplay.Username, highScoreDisplay.Score, null);
            
            DrawPropertiesExcluding (serializedObject, "Username", "Score");
        }
    }

}