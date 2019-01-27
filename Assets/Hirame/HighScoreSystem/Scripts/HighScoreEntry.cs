using UnityEngine;

namespace Hirame.HighScore
{
    [ExecuteInEditMode]
    public sealed class HighScoreEntry : MonoBehaviour
    {
        public ComplexString Ranking;
        public ComplexString PlayerName;
        public ComplexString PlayerScore;

        public void SetFontSize(int size)
        {
            if (Ranking.TextField != null)
                Ranking.TextField.fontSize = size;
            PlayerName.TextField.fontSize = size;
            PlayerScore.TextField.fontSize = size;
        }

        public void SetValues (int placement, string username, int score)
        {
            SetValues (placement, username, score.ToString ());
        }

        public void SetValues (int placement, string username, string score)
        {
            if (Ranking.TextField != null)
                Ranking.SetContent (placement.ToString ());
            PlayerName.SetContent (username);
            PlayerScore.SetContent (score);
        }


        [System.Serializable]
        public struct ComplexString
        {
            public TMPro.TMP_Text TextField;
            public string Prefix;
            public string Postfix;

            public void SetContent (string content)
            {
                TextField.text = $"{Prefix}{content}{Postfix}";
            }
        }
    }
}