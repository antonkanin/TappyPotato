using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Constants;
using PlayerClasses;

public class ScoreReader : MonoBehaviour
{
    public static ScoreReader Instance;

    void Awake()
    {
        Instance = this;
    }

    public IEnumerator SaveScore(string playerName, int score)
    {
        WWWForm form = new WWWForm();
        form.AddField(Const.NAME_FIELD, playerName);
        form.AddField(Const.SCORE_FIELD, score.ToString());

        UnityWebRequest request = UnityWebRequest.Post(Const.POST_URL, form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }


    public void GetScoreAsync(Action<IList<Player>> setScoreBoard)
    {
        StartCoroutine(GetScore(setScoreBoard));
    }

    IEnumerator GetScore(Action<IList<Player>> setScoreBoard)
    {
        WWW getRequest = new WWW(Const.GET_URL);
        yield return getRequest;

        if (!String.IsNullOrEmpty(getRequest.error))
        {
            Debug.Log("Could not connect to " + Const.GET_URL + ", error: " + getRequest.error);
            yield break;
        }

        string jsonResult = getRequest.text;
        jsonResult = fixJson(jsonResult);

        try
        {
            var scoreBoard = JsonUtility.FromJson<ScoreBoard>(jsonResult);
            var list = scoreBoard.Items;
            setScoreBoard(list);
        }
        catch (ArgumentException e)
        {
            Debug.Log("JSON parcing error: " + e.Message);
        }
    }

private WWW Get(string url)
{
    WWW resultWww = new WWW(url);
    while (!resultWww.isDone)
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);
    }

    return resultWww;
}

private string fixJson(string value)
{
    value = "{\"Items\":" + value + "}";
    return value;
}

}