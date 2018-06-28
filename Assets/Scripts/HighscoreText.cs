using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    [Serializable]
    class Player
    {
        public string Name;
        public string Score;
    }

    [Serializable]
    class ScoreBoard
    {
        public List<Player> Items;
    }


    private Text highScore;

    void OnEnable()
    {
        highScore = GetComponent<Text>();
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();


        ScoreBoard s = new ScoreBoard
        {
            Items = new List<Player>
            {
                new Player
                {
                    Name = "Anton",
                    Score = "100"
                },
                new Player
                {
                    Name = "Andrew",
                    Score = "1"
                }
            }
        };

        var r = JsonUtility.ToJson(s);

        var url = "http://localhost/game.php";

        WWW gameStats = new WWW(url);
        StartCoroutine(RequestGameStats(gameStats));
        
    }

    IEnumerator RequestGameStats(WWW www)
    {
        yield return www;

        string jsonResult = www.text;
        jsonResult = fixJson(jsonResult);


        var scoreBoard = JsonUtility.FromJson<ScoreBoard>(jsonResult);
        //var scoreBoard = JsonHelper.FromJson<Player>(jsonResult);
        foreach (var p in scoreBoard.Items)
        {
            Debug.Log(p.Name + " " + p.Score);
        }
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
