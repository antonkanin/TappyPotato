using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.JsonUtility;
using Utils;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    class Player
    {
        public string Name;
        public int Score;
    }

    private Text highScore;

    void OnEnable()
    {
        highScore = GetComponent<Text>();
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();

        string url = "http://localhost/game.php";

        WWW gameStats = new WWW(url);
        StartCoroutine(RequestGameStats(gameStats));
    }

    IEnumerator RequestGameStats(WWW www)
    {
        yield return www;

        string jsonResult = www.text;

        JsonHelper j;

    }
}
