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

    private string playerName_ = "";

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

    private int score_ = 0;
    private bool gameOver = true;

    public int Score
    {
        get { return score_; }
    }

    public float PositionX { get; set; }


    public bool GameOver
    {
        get { return gameOver; }
    }

    void Awake()
    {
        Instance = this;
        PositionX = 0;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
        //playerName_ = PlayerPrefs.GetString(Const.PLAYER_NAME_PREF);
    }

    void SetScoreBoardCallBack(IList<Player> scoreBoard)
    {
        scoreBoard_ = scoreBoard;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
    }

    void OnCountdownFinished()
    {
        SetPageState(PageState.None);
        scoreText.SetActive(true);
        OnGameStarted();
        score_ = 0;
        gameOver = false;
    }

    public void PlayerDied()
    {
        gameOver = true;

        UpdatePlayerScore();

        SetPageState(PageState.GameOver);
    }

    private void UpdatePlayerScore()
    {
        // we do not care about Player's name for now

        //if (playerName_ == "")
        //{
        //    enterNameDialog.SetActive(true);
        //}
        //else
        //{
        //    SavePlayerScoreIfNeeded(playerName_, score_);
        //}

        SavePlayerScoreIfNeeded(playerName_, score_);
    }

    public void PlayerScored()
    {
        score_++;
        scoreText.GetComponent<Text>().text = score_.ToString();
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

    public void SavePlayerName(string playerName)
    {
        playerName_ = playerName;
        //PlayerPrefs.SetString(Const.PLAYER_NAME_PREF, playerName);
        SavePlayerScoreIfNeeded(playerName, score_);
    }

    private void SavePlayerScoreIfNeeded(string playerName, int score)
    {
        int maxScore = PlayerPrefs.GetInt(Const.PLAYER_HIGH_SCORE_PREF, -1);
        if (score > maxScore)
        {
            score_ = score;
            PlayerPrefs.SetInt(Const.PLAYER_HIGH_SCORE_PREF, score);
            // ScoreReader.Instance.SaveScoreAsync(playerName, score);
        }

        // ScoreReader.Instance.GetScoreAsync(SetScoreBoardCallBack);
    }
}