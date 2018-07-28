using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class LogController : MonoBehaviour
{

    public GameObject logPanel;
    public GameObject logText;
    public GameObject showLogButton;
    public GameObject hideLogButton;

    private Text compText;

	void Start ()
	{
	    compText = logText.GetComponent<Text>();
	}

    public void ShowLog()
    {
        logPanel.SetActive(true);
        showLogButton.SetActive(false);
        hideLogButton.SetActive(true);
    }

    public void HideLog()
    {
        logPanel.SetActive(false);
        showLogButton.SetActive(true);
        hideLogButton.SetActive(false);
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        compText.text += "# " + logString + Environment.NewLine;
    }
}
