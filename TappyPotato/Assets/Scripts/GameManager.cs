using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.UI;
using PlayerClasses;
using UnityEngine.Experimental.XR.Interaction;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();

    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public GameObject scoreText;
    public GameObject pausePage;

    public bool GotScoreFromServer
    {
        get { return (scoreBoard_ != null); }
    } 

    public IList<Player> ScoreBoard
    {
        get { return scoreBoard_; }
    }

    private IList<Player> scoreBoard_ = null;

    enum GameUIState
    {
        Start,
        Playing,
        GameOver,
        Countdown,
        GamePaused
    }

    private bool gamePlaying = false;

    private int score_ = 0;

    public int Score
    {
        get { return score_; }
    }

    public float PositionX { get; set; }

    public bool GamePlaying
    {
        get { return gamePlaying; }
    }

    void Awake()
    {
        Instance = this;
    }

    void OnEnable()
    {
        CountdownText.OnCountdownFinished += OnCountdownFinished;
    }

    void OnDisable()
    {
        CountdownText.OnCountdownFinished -= OnCountdownFinished;
    }

    void OnCountdownFinished()
    {
        SetUIState(GameUIState.Playing);
        scoreText.SetActive(true);
        OnGameStarted();
        score_ = 0;
        PositionX = 0;
        gamePlaying = true;
    }

    public void PlayerDied()
    {
        if (gamePlaying)
        {
            gamePlaying = false;
            SavePlayerScoreIfNeeded();
            SetUIState(GameUIState.GameOver);
        }
    }

    public void PlayerScored()
    {
        score_++;
        scoreText.GetComponent<Text>().text = score_.ToString();
    }

    void SetUIState(GameUIState uiState)
    {
        switch (uiState)
        {
            case GameUIState.Playing:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                pausePage.SetActive(false);
                break;
            case GameUIState.Start:
                startPage.SetActive(true);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                pausePage.SetActive(false);
                break;
            case GameUIState.GameOver:
                startPage.SetActive(false);
                gameOverPage.SetActive(true);
                countDownPage.SetActive(false);
                pausePage.SetActive(false);
                break;
            case GameUIState.Countdown:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(true);
                pausePage.SetActive(false);
                break;
            case GameUIState.GamePaused:
                startPage.SetActive(false);
                gameOverPage.SetActive(false);
                countDownPage.SetActive(false);
                pausePage.SetActive(true);
                break;
        }
    }
    
    void SetScoreBoardCallBack(IList<Player> scoreBoard)
    {
        scoreBoard_ = scoreBoard;
    }

    public void ConfirmGameOver()
    {
        // activated when replay button is hit
        OnGameOverConfirmed(); // event sent to TapController
        scoreText.GetComponent<Text>().text = "0";
        SetUIState(GameUIState.Start);
    }

    public void StartGame()
    {
        // activated when play button is hit
        SetUIState(GameUIState.Countdown);
    }

    public void ResumeGame()
    {
        gamePlaying = true;
        SetUIState(GameUIState.Playing);
    }

    public void SaveScoreDebug()
    {
        SavePlayerScoreIfNeeded();
    }

    private void SavePlayerScoreIfNeeded()
    {
        int maxScore = PlayerPrefs.GetInt(Const.PLAYER_HIGH_SCORE_PREF, -1);
        if (score_ > maxScore)
        {
            PlayerPrefs.SetInt(Const.PLAYER_HIGH_SCORE_PREF, score_);
        }
        ScoreManager.Instance.SaveScore(score_, PositionX, Application.version);
    }
        
    void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("OnPauseEvent: " + pauseStatus);
        if (pauseStatus && gamePlaying)
        {
            gamePlaying = false;
            SetUIState(GameUIState.GamePaused);
        }
    }
}