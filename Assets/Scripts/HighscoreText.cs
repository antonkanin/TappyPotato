using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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

        var url = "http://localhost/game.php";

        WWW gameStats = new WWW(url);
        //StartCoroutine(RequestGameStats(gameStats));
        //StartCoroutine(PostResults());
        StartCoroutine(GetResults());
    }

    IEnumerator PostResults()
    {
        //List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        //formData.Add(new MultipartFormDataSection("name=anton&score=100"));
        WWWForm form = new WWWForm();
        form.AddField("name", "Anton");
        form.AddField("score", "100");

        UnityWebRequest request = UnityWebRequest.Post("http://localhost/score_post.php", form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }

    IEnumerator GetResults()
    {
        WWW gameStats = new WWW("http://localhost/game.php");
        yield return gameStats;

        string jsonResult = gameStats.text;
        Debug.Log(jsonResult);
        //jsonResult = fixJson(jsonResult);


        //var scoreBoard = JsonUtility.FromJson<ScoreBoard>(jsonResult);
        
        //foreach (var p in scoreBoard.Items)
        //{
        //    Debug.Log(p.Name + " " + p.Score);
        //}
    }

    string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}
