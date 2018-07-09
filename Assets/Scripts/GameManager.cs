using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using Constants;
using UnityEngine;
using UnityEngine.UI;
using PlayerClasses;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();

    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public GameObject enterNameDialog;
    public GameObject scoreText;

    private string playerName_;

    public bool GotScoreFromServer
    {
        get { return (scoreBoard_ != null); }
    } 

    public IList<Player> ScoreBoard
    {
        get { return scoreBoard_; }
    }

    private IList<Player> scoreBoard_ = null;

    enum PageState
    {
        None, 
        Start,
        GameOver,
        Countdown
    }

    private int score = 0;
    private bool gameOver = true;

    public int Score
    {
        get { return score; }
    }

    public bool GameOver
    {
        get { return gameOver; }
    }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        TapController.OnPlayerDied += OnPlayerDied;
        TapController.OnPlayerScored += OnPlayerScored;

        ScoreReader.Instance.GetScoreAsync(SetScoreBoardCallBack);
    }

    void SetScoreBoardCallBack(IList<Player> scoreBoard)
    {
        scoreBoard_ = scoreBoard;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
        TapController.OnPlayerDied -= OnPlayerDied;
        TapController.OnPlayerScored -= OnPlayerScored;
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        scoreText.SetActive(true);
        OnGameStarted();
        score = 0;
        gameOver = false;
    }

    void OnPlayerDied()
    {
        gameOver = true;
        ShowEnterNameDialogIfNeeded();
        SavePlayerScoreIfNeeded();
        SetPageState(PageState.GameOver);
    }

    void OnPlayerScored()
    {
        score++;
        scoreText.GetComponent<Text>().text = score.ToString();
    }

    void SetPageState(PageState state)
    {
        switch (state)
        {
            case PageState.None:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                break;
            case PageState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                break;
            case PageState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                break;
        }
    }

    public void ConfirmGameOver()
    {
        // activated when replay button is hit
        OnGameOverConfirmed(); // event sent to TapController
        scoreText.GetComponent<Text>().text = "0";
        SetPageState(PageState.Start);
    }

    public void StartGame()
    {
        // activated when play button is hit
        SetPageState(PageState.Countdown);
    }

    private void ShowEnterNameDialogIfNeeded()
    {
        if (!PlayerPrefs.HasKey(Const.PLAYER_NAME_PREF))
        {
            enterNameDialog.SetActive(true);
        }
    }

    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString(Const.PLAYER_NAME_PREF, playerName);
        SavePlayerScoreIfNeeded();
    }

    private void SavePlayerScoreIfNeeded()
    {
        int maxScore = PlayerPrefs.GetInt(Const.PLAYER_HIGH_SCORE_PREF, -1);
        if (score > maxScore)
        {
            PlayerPrefs.SetInt(Const.PLAYER_NAME_PREF, score);
            ScoreReader.Instance.SaveScore(Const.PLAYER_NAME_PREF, score);
        }
    }
}
