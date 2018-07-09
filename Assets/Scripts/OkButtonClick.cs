using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constants;

public class OkButtonClick : MonoBehaviour
{
    public GameObject enterScoreDialog;
    public InputField nameInputField;

    public void OnButtonClick()
    {
        string playerName = nameInputField.textComponent.text;
        GameManager.Instance.SavePlayerName(playerName);
        enterScoreDialog.SetActive(false);
    }
}
