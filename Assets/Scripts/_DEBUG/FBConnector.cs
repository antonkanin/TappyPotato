using System.Collections;
using System.Collections.Generic;
using Constants;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.Experimental.Audio;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FBConnector : MonoBehaviour
{
    public GameObject dialogLoggedOut;
    public GameObject dialogLoggedIn;
    public GameObject dialogUserName;

    public static FBConnector Instance;

    void Start()
    {
        // FB.Init(SetInit, OnHideUnity);
        FB.Init(appId: "219403772103872", clientToken: null, cookie: true, logging: true,
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
            //FB.API("/me/picture&type=square&height=128&width=128", HttpMethod.GET, DiplayProfilePic);
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

        form.AddField(Const.ACCESS_TOKEN, accessToken);
        form.AddField(Const.SCORE_FIELD, score);

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
