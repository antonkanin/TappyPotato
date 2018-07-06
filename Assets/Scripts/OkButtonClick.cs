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
        int score = GameManager.Instance.Score;
        PlayerPrefs.SetString(Const.PLAYER_NAME_PREF, playerName);
        StartCoroutine(ScoreUtils.ScoreManager.SaveScore(playerName, score));
        enterScoreDialog.SetActive(false);
    }
}
