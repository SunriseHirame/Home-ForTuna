using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using AlchemyEngine;
using UnityEngine;
using UnityEngine.Networking;

namespace Hirame.HighScore
{
    public sealed class HighScore : MonoBehaviour
    {
        private const string HighScoreURL = "http://dreamlo.com/lb/";
        private const string PrivateCode = "DcKqBO_Ga0qduA5dWU7_SAeQ8mA3U21UW4RpYl6AwYFg";
        private const string PublicCode = "5c4d9301b6397e0c24bca671";

        public const string HighScoreUrl = HighScoreURL + PrivateCode;

        public static HighScore Instance { get; private set; }
        
        private static PlayerHighScoreData CurrentPlayerLastData;
        
        public static event Action<int, int> HighScoresUpdated;

        public static int ScoresCount { get; private set; }

        [SerializeField]
        private PlayerHighScoreData[] HighScores = new PlayerHighScoreData[1000];

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize ()
        {
            Instance = new GameObject ("HighScore").AddComponent<HighScore> ();
        }

        public static PlayerHighScoreData GetPlayerData (int index)
        {
            return Instance.HighScores[index];
        }

        public static string GetLastPlayerName ()
        {
            return CurrentPlayerLastData.Username;
        }

        public static int FindPlacementInLocalData (string username)
        {
            var scores = Instance.HighScores;

            for (var i = 0; i < ScoresCount; i++)
            {
                if (scores[i].Equals (username))
                    return i;
            }

            return -1;
        }

        public static void SetPlayerName (TMPro.TMP_InputField username)
        {
            SetPlayerName (username.text);
        }

        public static void SetPlayerName(TMPro.TMP_Text username)
        {
            SetPlayerName (username.text);
        }

        public static void SetPlayerName (string playerName)
        {
            CurrentPlayerLastData = new PlayerHighScoreData
            {
                Username = playerName
            };
            if (string.IsNullOrEmpty(CurrentPlayerLastData.Username))
                return;
            
            PlayerPrefs.SetString ("LastUserName", CurrentPlayerLastData.Username);
            PlayerPrefs.Save ();
        }

        public static void GetPlayersScore (string username, Action<PlayerHighScoreData> callback)
        {
            Instance.StartCoroutine (Instance.Internal_FetchScoreForPlayer (username, callback));
        }

        public static bool UploadScore (string username, int score, Action<bool> callback)
        {
            return UploadScore (username, score.ToString (), callback);
        }

        public static bool UploadScore (string username, string score, Action<bool> callback)
        {
            if (!HighScoresUtility.ValidateUserDataString (username)
                || !HighScoresUtility.ValidateUserDataString (score))
            {
                Debug.LogError (
                    $"[{nameof (HighScoreManager)}]: Provided 'username' or 'score' was null of empty, or it contained invalid characters!");
                callback.Invoke (false);
                return false;
            }

            if (!HighScoresUtility.IsSamePlayer (CurrentPlayerLastData, username)
                && HighScoresUtility.CompareScores (CurrentPlayerLastData.Score, score) < 0)
            {
                CurrentPlayerLastData.Score = score;
            }
            else
            {
                CurrentPlayerLastData = new PlayerHighScoreData
                {
                    Username = username,
                    Score = score
                };
            }

            Instance.StartCoroutine (Instance.Internal_PostHighScore (username, score, callback));
            return true;
        }


        public static void UpdateHighScoresFromServer ()
        {
            Instance.StartCoroutine (Instance.Internal_UpdateHighScores ());
        }

        public static void UpdateHighScoresFromServer (int count)
        {
            Instance.StartCoroutine (Instance.Internal_UpdateHighScores (count));
        }


        /// <summary>
        /// Note: 0 based indexing!
        /// </summary>
        /// <param name="start"></param>
        /// <param name="count"></param>
        public static void UpdateHighScoresFromServer (int start, int count)
        {
            Instance.StartCoroutine (Instance.Internal_UpdateHighScores (start, count));
        }

        public override void OnSingletonCreated ()
        {
            var lastUser = PlayerPrefs.GetString ("LastUserName", string.Empty);

            if (string.IsNullOrEmpty (lastUser))
                return;
            
            CurrentPlayerLastData.Username = lastUser;
        }

        private void OnDisable ()
        {
            Debug.Log(CurrentPlayerLastData.Username);
            if (!string.IsNullOrEmpty (CurrentPlayerLastData.Username))
                PlayerPrefs.SetString ("LastUserName", CurrentPlayerLastData.Username);
            PlayerPrefs.Save ();
            Debug.Log(PlayerPrefs.GetString("LastUserName", "NOT FOUND"));
        }

        #region Internal Handlers

        private IEnumerator Internal_FetchScoreForPlayer (
            string username, Action<PlayerHighScoreData> callback)
        {
            using (var webRequest = new UnityWebRequest (
                $"{HighScoreURL}{PrivateCode}/pipe-get/{username}"))
            {
                yield return webRequest.SendWebRequest ();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError ($"[{nameof (HighScoreManager)}]: Failed to fetch user's score! {webRequest.error}");
                    callback?.Invoke (new PlayerHighScoreData ());
                    yield break;
                }

                if (!HighScoresUtility.TryParsePlayerData (webRequest.downloadHandler.text, -1, out var playerData))
                    yield break;

                Debug.Log (webRequest.downloadHandler.text);
                callback?.Invoke (playerData);
            }
        }

        private IEnumerator Internal_PostHighScore (string username, string score, Action<bool> callback)
        {
            //Debug.Log ($"[{nameof (HighScoreManager)}]: Started high score upload for: {username}, {score}");

            using (var webRequest = new UnityWebRequest (
                $"{HighScoreURL}{PrivateCode}/add/{username}/{score}"))
            {
                yield return webRequest.SendWebRequest ();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError (
                        $"[{nameof (HighScoreManager)}]: Failed to upload user's score! {webRequest.error}");
                    callback?.Invoke (false);
                    yield break;
                }

                callback?.Invoke (true);
            }
        }

        private IEnumerator Internal_UpdateHighScores (int count)
        {
            //Debug.Log ($"[{nameof (HighScoreManager)}]: Started high score update from server.");

            using (var webRequest = UnityWebRequest.Get (
                $"{HighScoreURL}/{PublicCode}/pipe/{count.ToString ()}"))
            {
                yield return webRequest.SendWebRequest ();
                yield return webRequest.downloadHandler;

                //Debug.Log (webRequest.downloadHandler.isDone.ToString());

                if (webRequest.isNetworkError)
                {
                    Debug.LogError (
                        $"[{nameof (HighScoreManager)}]: Failed to upload user's score! {webRequest.error}");
                    yield break;
                }

                //Debug.Log ($"WEB {webRequest.downloadHandler?.text ?? "NULL"}");
                //Debug.Log ($"[{nameof (HighScoreManager)}]: High score update finished!");
                ScoresCount = HighScoresUtility.FormatAndUpdateData (webRequest.downloadHandler.text, 0, HighScores);

                HighScoresUpdated?.Invoke (0, ScoresCount);
            }
        }

        private IEnumerator Internal_UpdateHighScores ()
        {
            //Debug.Log ($"[{nameof (HighScoreManager)}]: Started high score update from server.");

            using (var webRequest = UnityWebRequest.Get (
                $"{HighScoreURL}/{PublicCode}/pipe"))
            {
                yield return webRequest.SendWebRequest ();
                yield return webRequest.downloadHandler;

                //Debug.Log (webRequest.downloadHandler.isDone.ToString());

                if (webRequest.isNetworkError)
                {
                    Debug.LogError (
                        $"[{nameof (HighScoreManager)}]: Failed to upload user's score! {webRequest.error}");
                    yield break;
                }

                //Debug.Log ($"WEB {webRequest.downloadHandler?.text ?? "NULL"}");
                //Debug.Log ($"[{nameof (HighScoreManager)}]: High score update finished!");
                ScoresCount = HighScoresUtility.FormatAndUpdateData (webRequest.downloadHandler.text, 0, HighScores);

                HighScoresUpdated?.Invoke (0, ScoresCount);
            }
        }

        private IEnumerator Internal_UpdateHighScores (int from, int count)
        {
            //Debug.Log ($"[{nameof (HighScoreManager)}]: Started high score update from server.");

            using (var webRequest = UnityWebRequest.Get (
                $"{HighScoreURL}/{PublicCode}/pipe/{from.ToString ()}/{count.ToString ()}"))
            {
                yield return webRequest.SendWebRequest ();
                yield return webRequest.downloadHandler;

                //Debug.Log (webRequest.downloadHandler.isDone.ToString());

                if (webRequest.isNetworkError)
                {
                    Debug.LogError (
                        $"[{nameof (HighScoreManager)}]: Failed to upload user's score! {webRequest.error}");
                    yield break;
                }

                //Debug.Log ($"WEB {webRequest.downloadHandler?.text ?? "NULL"}");
                //Debug.Log ($"[{nameof (HighScoreManager)}]: High score update finished!");
                ScoresCount = HighScoresUtility.FormatAndUpdateData (webRequest.downloadHandler.text, from, HighScores);

                HighScoresUpdated?.Invoke (from, count);
            }
        }

        #endregion


        #region Editor Helpers

#if UNITY_EDITOR
        public static void ClearAllScores ()
        {
            Instance.StartCoroutine (Instance.Internal_ClearScores ());
        }

        private IEnumerator Internal_ClearScores ()
        {
            using (var webRequest = new UnityWebRequest (
                $"{HighScoreURL}{PrivateCode}/clear"))
            {
                yield return webRequest.SendWebRequest ();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError (
                        $"[{nameof (HighScoreManager)}]: Failed to upload user's score! {webRequest.error}");
                    yield break;
                }

                Debug.Log ($"[{nameof (HighScoreManager)}]: Scores Cleared.");

                for (var i = 0; i < HighScores.Length; i++)
                {
                    HighScores[i] = default (PlayerHighScoreData);
                }
            }
        }

#endif

        #endregion
    }

    [System.Serializable]
    public struct PlayerHighScoreData : IComparable<string>, IEquatable<string>
    {
        public string Username;
        public string Score;
        public int Ranking;

        public int CompareTo (string other)
        {
            return Username?.CompareTo (other) ?? int.MaxValue;
        }

        public bool Equals (string other)
        {
            return Username?.Equals (other) ?? false;
        }
    }
}