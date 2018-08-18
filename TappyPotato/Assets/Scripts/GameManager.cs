using System.Collections.Generic;
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
    public GameObject scoreText;

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
        SetPageState(PageState.None);
        scoreText.SetActive(true);
        OnGameStarted();
        score_ = 0;
        PositionX = 0;
        gameOver = false;
    }

    public void PlayerDied()
    {
        gameOver = true;
        SavePlayerScoreIfNeeded();
        SetPageState(PageState.GameOver);
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
    
    void SetScoreBoardCallBack(IList<Player> scoreBoard)
    {
        scoreBoard_ = scoreBoard;
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
        ScoreManager.Instance.SaveScore(score_, PositionX);
    }
}