using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipButtonClick : MonoBehaviour
{
    public GameObject enterScoreDialog;

    public void OnSkipClick()
    {
        Debug.Log("OnSkipClick()");
        enterScoreDialog.SetActive(false);
    }
}
