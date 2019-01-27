using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hirame.HighScore;
using UnityEngine;

namespace Hirame
{
    public static class HighScoresUtility
    {
        
        public static bool ValidateUserDataString (string toValidate)
        {
            return (!string.IsNullOrEmpty (toValidate) && toValidate.All (char.IsLetterOrDigit));
        }

        public static int FormatAndUpdateData (string data, int startIndex, PlayerHighScoreData[] highScores)
        {
            var entries = data.Split ('\n');
            var scoresCount = entries.Length;

            for (var i = 0; i < entries.Length; i++)
            {
                if (string.IsNullOrWhiteSpace (entries[i])
                    || !TryParsePlayerData (entries[i], startIndex, out var playerData))
                {
                    scoresCount--;
                    continue;
                }

                startIndex++;
                highScores[i] = playerData;
            }

            return scoresCount;
        }

        public static bool TryParsePlayerData (string data, int placement, out PlayerHighScoreData playerData)
        {
            var splitData = data.Split ('|');
            if (splitData.Length == 0)
            {
                playerData = default (PlayerHighScoreData);
                return false;
            }

            playerData = new PlayerHighScoreData
            {
                Username = splitData[0],
                Score = splitData[1],
                Ranking = placement != -1 ? placement + 1 : -1
            };
            return true;
        }

        public static bool IsSamePlayer (PlayerHighScoreData data, string username)
        {
            if (username == null)
                return false;
            return data.Username?.Equals (username) ?? false;
        }

        public static int CompareScores (string lScore, string rScore)
        {
            if (!int.TryParse (lScore, out var score1)
                || int.TryParse (rScore, out var score2))
            {
                return int.MaxValue;
            }
            return score1 - score2;
        }

    }

}