using UnityEngine;
using UnityEngine.UI;
using Constants;

[RequireComponent(typeof(Text))]
public class HighscoreText : MonoBehaviour
{
    private Text highScore;
    void OnEnable()
    {
        highScore = GetComponent<Text>();
        highScore.text = "High Score: " + PlayerPrefs.GetInt(Const.PLAYER_HIGH_SCORE_PREF).ToString();
    }
}