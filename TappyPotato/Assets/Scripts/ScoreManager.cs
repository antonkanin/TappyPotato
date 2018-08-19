using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using Constants;
using PlayerClasses;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void SaveScore(int score, float positionX)
    {
        string token;
        if (FacebookManager.GetAccessToken(out token))
        {
            Debug.Log("Client Token: " + token);
            StartCoroutine(SaveScoreAsync(token, score, positionX));
        }
        else
        {
            Debug.Log("Facebook not logged in");
        }
    }

    private IEnumerator SaveScoreAsync(string accessToken, int score, float positionX)
    {
        WWWForm form = new WWWForm();

        using (Aes myAes = Aes.Create())
        {
            myAes.KeySize = 128;
            var encryptedToken = CryptoUtils.AESEncrypt(accessToken, myAes.Key, myAes.IV);

            string encryptedTokenString = Convert.ToBase64String(encryptedToken);
            string keyString = Convert.ToBase64String(myAes.Key);
            string IVString = Convert.ToBase64String(myAes.IV);

            form.AddField(Const.ACCESS_TOKEN, encryptedTokenString);
            form.AddField(Const.AES_KEY, keyString);
            form.AddField(Const.AES_IV, IVString);
            form.AddField(Const.SCORE_FIELD, score);
            int positionX_int = Mathf.RoundToInt(positionX * 10);
            form.AddField(Const.POSITIONX_FIELD, positionX_int);
        }

        UnityWebRequest request = UnityWebRequest.Post(Const.POST_URL, form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }

    public void GetScoreAsync(Action<IList<Player>> LoadPlayers)
    {
        StartCoroutine(GetScore(LoadPlayers));
    }

    IEnumerator GetScore(Action<IList<Player>> setScoreBoard)
    {
        var randomString = UnityEngine.Random.Range(1000000, 8000000).ToString();
        WWW getRequest = new WWW(Const.GET_URL + "?t=" + randomString);

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

    private string fixJson(string value)
    {
        value = "{\"Items\":" + value + "}";
        return value;
    }
}