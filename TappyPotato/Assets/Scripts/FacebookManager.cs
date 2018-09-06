using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Facebook.Unity;
using Constants;

public class FacebookManager : MonoBehaviour
{
    public GameObject loginFBButton;
    public GameObject loginFBText;

    void Start()
    {
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
            GameManager.Instance.SaveScore();
        }
    }

    public static bool GetAccessToken(out string token)
    {
        if (FB.IsLoggedIn)
        {
            token = AccessToken.CurrentAccessToken.TokenString;
            return true; 
        }
        else
        {
            token = "";
            Debug.Log("Facebook not logged in");
            return false;
        }
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

    void DealWithFBMenus(bool isLoggedIn)
    {
        if (isLoggedIn)
        {
            loginFBButton.SetActive(false);
            loginFBText.SetActive(false);

            // FB.API("/me?fields=first_name", HttpMethod.GET, DisplayUserName);
        }
    }

}
