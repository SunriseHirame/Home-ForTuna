using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hirame.HighScore
{
    public sealed class HighScoreDisplay : MonoBehaviour
    {
        public bool RefreshOnEnable = true;
        public bool FetchLastPlayerName = true;
        
        //public HighScoreEntry Proto;
        public RectTransform Container;

        //public Guid PlayerId;
        public string Username;
        public string Score;

        public int StartFrom = 0;
        
        public List<HighScoreEntry> Entries;

        public UnityEvent ScoreUploadedSuccessfully;
        public UnityEvent ScoresUpdated;
/*
        public void GenerateNewId ()
        {
            //PlayerId = Guid.NewGuid ();
        }
        */

        public void NextPage ()
        {
            StartFrom = Mathf.Min(HighScore.ScoresCount - Entries.Count, StartFrom + Entries.Count);
            UpdateScoreDisplay();
        }

        public void PrivousPage()
        {
            StartFrom = Mathf.Max (0, StartFrom - Entries.Count);
            UpdateScoreDisplay();
        }

        public void SendScore ()
        {
            HighScore.UploadScore (Username, Score, OnScoreUploaded);
        }

        public void RefreshScores ()
        {
            HighScore.UpdateHighScoresFromServer ();
        }
        
        private void OnScoreUploaded (bool succeeded)
        {
            Debug.Log ($"Score Uploaded. {succeeded.ToString ()}");
            
            if (succeeded)
                ScoreUploadedSuccessfully.Invoke ();
        }

        private void OnHighScoresUpdated (int start, int count)
        {
            UpdateScoreDisplay();
            ScoresUpdated.Invoke ();
        }

        private void UpdateScoreDisplay ()
        {
            var scores = HighScore.ScoresCount;

            for (var i = 0; i < Entries.Count; i++)
            {
                if (StartFrom + i < scores)
                {
                    var score = HighScore.GetPlayerData(StartFrom + i);
                    Entries[i].SetValues(score.Ranking, score.Username, score.Score);
                    Entries[i].gameObject.SetActive(true);
                }
                else
                    Entries[i].gameObject.SetActive(false);
            }

        }

        private void OnEnable ()
        {
            HighScore.HighScoresUpdated += OnHighScoresUpdated;

            if (FetchLastPlayerName)
                Username = HighScore.GetLastPlayerName ();

            if (RefreshOnEnable)
                RefreshScores ();
        }

        private void OnDisable ()
        {
            HighScore.HighScoresUpdated -= OnHighScoresUpdated;
        }

#if UNITY_EDITOR
        
        [ContextMenu ("Add From Container")]
        private void AddFromContainer ()
        {
            if (Container == null)
                return;
            
            Entries.Clear ();
            var childCount = Container.childCount;

            for (var i = 0; i < childCount; i++)
            {
                var entry = Container.GetChild (i).GetComponent<HighScoreEntry> ();
                
                if (entry == null)
                    continue;
                
                Entries.Add (entry);
            }
        }
        
#endif //UNITY_EDITOR
        
    }

}