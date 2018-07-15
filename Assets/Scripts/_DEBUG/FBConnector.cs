using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.UI;

public class FBConnector : MonoBehaviour
{
    public GameObject dialogLoggedOut;
    public GameObject dialogLoggedIn;
    public GameObject dialogUserName;

    void Start()
    {
        FB.Init(SetInit, OnHideUnity);
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
