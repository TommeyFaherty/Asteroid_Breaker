using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<ScoreBoardEntry> scoreBoardEntryList;
    private List<Transform> scoreBoardEntryTransformList;
    public Text currNameText;
    public Text currScoreText;
    private int currScore;
    private string currName;

    private void Awake()
    {
        entryContainer = transform.Find("ScoreBoardEntryContainer");
        entryTemplate = entryContainer.Find("ScoreBoardEntryTemplate");
    }

    public void GetScoreBoard()
    {
        //Get current players name and score 
        currName = PlayerPrefs.GetString("player_name");
        currScore = PlayerPrefs.GetInt("player_score");

        currNameText.text = currName;
        currScoreText.text = currScore.ToString();

        AddHighScoreEntry(currScore, currName);

        //Get Scores from json
        string jsonString = PlayerPrefs.GetString("scoreBoard");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        scoreBoardEntryList = highScores.scoreBoardEntryList;

        //Sort entry list  by Score     
        for (int i = 0; i < scoreBoardEntryList.Count; i++)
        {
            for (int j = i + 1; j < scoreBoardEntryList.Count; j++)
            {
                if (scoreBoardEntryList[j].score > scoreBoardEntryList[i].score)
                {
                    ScoreBoardEntry temp = scoreBoardEntryList[i];
                    scoreBoardEntryList[i] = scoreBoardEntryList[j];
                    scoreBoardEntryList[j] = temp;
                }
            }
        }

        scoreBoardEntryTransformList = new List<Transform>();
        foreach (ScoreBoardEntry scoreBoardEntry in scoreBoardEntryList)
        {
            CreateScoreBoardEntryTransform(scoreBoardEntry, entryContainer, scoreBoardEntryTransformList);
        }
    }

    private void CreateScoreBoardEntryTransform(ScoreBoardEntry scoreBoardEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 20f;
        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        //entryTemplate.gameObject.SetActive(true);

        //Setting ranks
        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            default: rankString = rank + "TH"; break;
            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
            case 4: rankString = "4TH"; break;
            case 5: rankString = "5TH"; break;
            case 6: rankString = "6TH"; break;
            case 7: rankString = "7TH"; break;
            case 8: rankString = "8TH"; break;
            case 9: rankString = "9TH"; break;
            case 10: rankString = "10TH"; break;
        }

        entryTransform.Find("PositionText").GetComponent<Text>().text = rankString;

        //get Score and name passed in
        int tempScore = scoreBoardEntry.score;
        string tempName = scoreBoardEntry.name;

        entryTransform.Find("ScoreText").GetComponent<Text>().text = tempScore.ToString();
        entryTransform.Find("NameText").GetComponent<Text>().text = tempName;

        transformList.Add(entryTransform);
    }

    private void AddHighScoreEntry(int score, string name)
    {
        //Create ScoreBoard Entry
        ScoreBoardEntry scoreBoardEntry = new ScoreBoardEntry { score = score, name = name };

        //Load saved Highscores
        string jsonString = PlayerPrefs.GetString("scoreBoard");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        //scoreBoardEntryList = highScores.scoreBoardEntryList;

        //Ensure only top 10 scores are saved
        if (highScores.scoreBoardEntryList.Count == 10 && scoreBoardEntry.score > highScores.scoreBoardEntryList[9].score)
        {
            //Replace last place with new entry
            highScores.scoreBoardEntryList.RemoveAt(9);
            highScores.scoreBoardEntryList.Add(scoreBoardEntry);
        }
        else if(highScores.scoreBoardEntryList.Count < 10)
        {
            //If scoreBoard has less than 10 players
            //Add new highscore
            highScores.scoreBoardEntryList.Add(scoreBoardEntry);
        }

        //Save updated scoreBoard
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("scoreBoard", json);
        PlayerPrefs.Save();
    }

    private class HighScores
    {
        public List<ScoreBoardEntry> scoreBoardEntryList;
    }

    [System.Serializable]
    private class ScoreBoardEntry
    {
        public int score;
        public string name;
    }
}
