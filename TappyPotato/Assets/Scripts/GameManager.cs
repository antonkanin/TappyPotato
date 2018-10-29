using System.Collections.Generic;
using Constants;
using UnityEngine;
using UnityEngine.UI;
using PlayerClasses;

public class GameManager : MonoBehaviour
{
    public delegate void GameDelegate();

    public static event GameDelegate OnGameStarted;
    public static event GameDelegate OnGameResumed;
    public static event GameDelegate OnGameOverConfirmed;

    public static GameManager Instance;

    public GameObject startPage;
    public GameObject gameOverPage;
    public GameObject countDownPage;
    public GameObject scoreText;
    public GameObject pausePage;

    public GameState gameState;

    public FloatVariable potatoHorizontalPosition;

    public bool GotScoreFromServer
    {
        get { return (scoreBoard_ != null); }
    } 

    public IList<Player> ScoreBoard
    {
        get { return scoreBoard_; }
    }

    private IList<Player> scoreBoard_ = null;

    private int score_ = 0;

    public int Score
    {
        get { return score_; }
    }

    // GameStateManager
    // - states (playing / not playing)

    // GameStatistics
    // - Player Score
    // - Player Position

    // 3rd entity (GameEventsManager)
    // -- watch super presentation about scriptable objects & events

    // ScoreManager
    // -- saves score locally
    // -- saves score to the server

    void Awake()
    {
        Instance = this;
        currentPage = startPage;
        gameState.state = GameState.State.notPlaying;
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
        OnGameStarted?.Invoke();
        ;
        score_ = 0;
        potatoHorizontalPosition.Value = 0;
    }

    public void PlayerDied()
    {
        SavePlayerScoreIfNeeded();
        SetUIState(GameUIState.GameOver);
    }

    public void PlayerScored()
    {
        score_++;
        scoreText.GetComponent<Text>().text = score_.ToString();
    }

    private GameObject currentPage;

    void SetUIState(GameUIState uiState)
    {
        GameObject previousCurrentPage = currentPage;

        switch (uiState)
        {
            case GameUIState.Playing:
                gameState.state = GameState.State.playing;
                currentPage = null;
                break;
            case GameUIState.Start:
                gameState.state = GameState.State.notPlaying;
                currentPage = startPage;
                break;
            case GameUIState.GameOver:
                gameState.state = GameState.State.notPlaying;
                currentPage = gameOverPage;
                break;
            case GameUIState.Countdown:
                gameState.state = GameState.State.notPlaying;
                currentPage = countDownPage;
                break;
            case GameUIState.GamePaused:
                gameState.state = GameState.State.paused;
                currentPage = pausePage;
                break;
        }

        if (previousCurrentPage != currentPage)
        {
            previousCurrentPage?.SetActive(false);
            currentPage?.SetActive(true);
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
        SetUIState(GameUIState.Playing);
        OnGameResumed();
    }

    private void SavePlayerScoreIfNeeded()
    {
        int maxScore = PlayerPrefs.GetInt(Const.PLAYER_HIGH_SCORE_PREF, -1);
        if (score_ > maxScore)
        {
            PlayerPrefs.SetInt(Const.PLAYER_HIGH_SCORE_PREF, score_);
        }
        SaveScore();
    }

    public void SaveScore()
    {
        // dependencies from other objects as ScoreManager, countdown pages and bla bla bla should be removed
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.SaveScore(score_, potatoHorizontalPosition.Value, Application.version);    
        }
    }
        
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && gameState.state == GameState.State.playing)
        {
            SetUIState(GameUIState.GamePaused);
        }
    }
}