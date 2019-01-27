using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hirame.HighScore;
using System;
using TMPro;

public class UpdateScores : MonoBehaviour
{
    public Score PlayerScore;
    public List<HighScoreEntry> ScoreEntries;
    public int LastPlayerFontSize = (int) (46 * 1.618f);
    public int DefaultFontSize = 46;
    public int LatestRank;
    public Color PlayerColor = Color.yellow;

    public UnityEngine.Events.UnityEvent HighScoresUpdated;

    private Color defaultColor;

    private void OnEnable()
    {
        HighScore.UploadScore(
            HighScore.GetLastPlayerName (), PlayerScore.CurrentScore, OnScoreUploaded);
        HighScore.HighScoresUpdated += OnHighScoresUpdated;

        foreach (var entry in ScoreEntries)
            entry.gameObject.SetActive(false);
    }

    private void OnDisable ()
    {
        HighScore.HighScoresUpdated -= OnHighScoresUpdated;
    }

    void OnHighScoresUpdated (int from, int count)
    {
        var ownIndex = HighScore.FindPlacementInLocalData(
            HighScore.GetLastPlayerName ());

        var scoresCount = HighScore.ScoresCount;


        var startIndex = Mathf.Max(0, ownIndex - ScoreEntries.Count + 2);
        var stopIndex = Mathf.Max (ownIndex + 1, scoresCount - 1);

        var itemsToDisplay = Mathf.Min(ScoreEntries.Count, stopIndex - startIndex + 2);
        itemsToDisplay = Math.Min(scoresCount, itemsToDisplay);

        print(itemsToDisplay.ToString ());
        var ind = 0;
        for (ind = 0; ind < itemsToDisplay; ind++)
        {
            var data = HighScore.GetPlayerData(startIndex++);

            ScoreEntries[ind].gameObject.SetActive(true);
            ScoreEntries[ind].SetValues(data.Ranking, data.Username, data.Score);
            var textField = ScoreEntries[ind].PlayerName.TextField;
            var scoreField = ScoreEntries[ind].PlayerScore.TextField;
            var rankField = ScoreEntries[ind].Ranking.TextField;

            if (ownIndex == startIndex - 1)
            {
                textField.enableAutoSizing = true;
                textField.fontSizeMax = LastPlayerFontSize;
                textField.color = PlayerColor;
                scoreField.color = PlayerColor;
                rankField.color = PlayerColor;
            }
            else
            {
                ScoreEntries[ind].SetFontSize(DefaultFontSize);
                textField.color = defaultColor;
                scoreField.color = defaultColor;
                rankField.color = defaultColor;
            }

        }

        for (; ind < ScoreEntries.Count; ind++)
        {
            ScoreEntries[ind].gameObject.SetActive(false);
        }

    }

    void OnScoreUploaded(bool succes)
    {
        //HighScoreManager.UpdateHighScoresFromServer(Scoreboard.Entries.Count);
        HighScore.UpdateHighScoresFromServer();
    }

    public void UpdatePlayerName(string name)
    {
        HighScore.SetPlayerName(name);
    }
}