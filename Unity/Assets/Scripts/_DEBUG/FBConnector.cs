﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Facebook.Unity;
using Constants;

public class FBConnector : MonoBehaviour
{
    public GameObject dialogLoggedOut;
    public GameObject dialogLoggedIn;
    public GameObject dialogUserName;

    public static FBConnector Instance;

    void Start()
    {
        // FB.Init(SetInit, OnHideUnity);
        FB.Init(appId: Const.FBAppID, clientToken: null, cookie: true, logging: true,
            status: true, xfbml: false, frictionlessRequests: true,
            authResponse: null, javascriptSDKLocale: "en_US",
            onHideUnity: OnHideUnity, onInitComplete: SetInit);
    }

    private void SetInit()
    {
        CheckIfFBLoggedIn();
        DealWithFBMenus(FB.IsLoggedIn);
    }

    private void OnHideUnity(bool isGameDown)
    {
        if (!isGameDown)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void FBLogin()
    {
        List<string> permissions = new List<string>();
        permissions.Add("public_profile");

        FB.LogInWithReadPermissions(permissions, AuthCallBack);
    }

    private void CheckIfFBLoggedIn()
    {
        if (FB.IsLoggedIn)
        {
            Debug.Log("Facebook is logged in");
        }
        else
        {
            Debug.Log("Facebook is not logged in");
        }
    }

    private void AuthCallBack(IResult result)
    {
        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else
        {
            CheckIfFBLoggedIn();
            DealWithFBMenus(FB.IsLoggedIn);
        }
    }

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            dialogLoggedIn.SetActive(true);
            dialogLoggedOut.SetActive(false);

            FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
        }
        else
        {
            dialogLoggedIn.SetActive(false);
            dialogLoggedOut.SetActive(true);
        }
    }

    public void SaveScore(int score)
    {
        if (FB.IsLoggedIn)
        {
            var accessToken = AccessToken.CurrentAccessToken;
            Debug.Log("Client Token: " + accessToken.TokenString);
            StartCoroutine(SaveScoreAsync(accessToken.TokenString, 10));
        }
        else
        {
            Debug.Log("Facebook not logged in");
        }
    }

    private IEnumerator SaveScoreAsync(string accessToken, int score)
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
        }

        UnityWebRequest request = UnityWebRequest.Post(Const.POST_URL, form);
        yield return request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }

    public void SaveScoreTest()
    {
        SaveScore(10);
    }

    private void DisplayUserName(IResult result)
    {
        Text textUserName = dialogUserName.GetComponent<Text>();

        if (result.Error == null)
        {
            textUserName.text = "Hi there, " + result.ResultDictionary["first_name"];
        }
        else
        {
            Debug.Log(result.Error);
        }
    }

}
