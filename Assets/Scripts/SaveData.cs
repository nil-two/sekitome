using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System;

public class SaveData : MonoBehaviour
{
    public static ScoreRecord[] GetScoreRecords(int nRecords)
    {
        var scoreRecords = new List<ScoreRecord>();

        for (var i = 0; i < nRecords; i++)
        {
            ScoreRecord scoreRecord = new ScoreRecord();
            scoreRecord.score = PlayerPrefs.GetInt($"scores.{i}.score",   0);
            scoreRecord.date  = PlayerPrefs.GetString($"scores.{i}.date", "");

            scoreRecords.Add(scoreRecord);
        }

        return scoreRecords.ToArray();
    }

    public static bool RecordScore(int nRecords, int score, string date)
    {
        var scoreRecords  = new List<ScoreRecord>();
        var scoreRecorded = false;

        for (var i = 0; i < nRecords; i++)
        {
            ScoreRecord scoreRecord = new ScoreRecord();
            scoreRecord.score = PlayerPrefs.GetInt($"scores.{i}.score",   0);
            scoreRecord.date  = PlayerPrefs.GetString($"scores.{i}.date", "");

            scoreRecords.Add(scoreRecord);

            if (score > scoreRecord.score)
            {
                scoreRecorded = true;
            }
        }

        ScoreRecord newScoreRecord = new ScoreRecord();
        newScoreRecord.score = score;
        newScoreRecord.date  = date;

        scoreRecords.Add(newScoreRecord);
        scoreRecords.Sort((a, b) => {
            var d1 = b.score - a.score;
            if (d1 != 0) return d1;

            var d2 = String.Compare(Regex.Replace(b.date, "[^/]", ""), Regex.Replace(a.date, "[^/]", ""));
            if (d2 != 0) return d2;

            return 0;
        });

        for (var i = 0; i < nRecords; i++)
        {
            PlayerPrefs.SetInt($"scores.{i}.score",   scoreRecords[i].score);
            PlayerPrefs.SetString($"scores.{i}.date", scoreRecords[i].date);
        }

        return scoreRecorded;
    }
}
