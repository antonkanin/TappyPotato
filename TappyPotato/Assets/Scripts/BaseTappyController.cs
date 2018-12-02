using System;
using UnityEngine;

public abstract class BaseTappyController : MonoBehaviour
{
    public GameState gameState;

    void Update()
    {
        if (gameState == null)
        {
            throw new Exception(this.GetType() + ": game manager can not be null");
        }

        switch (gameState.CurrentState)
        {
            case GameState.State.playing:
                ActiveUpdate();
                break;
            case GameState.State.paused:
                PausedUpdate();
                break;
            case GameState.State.notPlaying:
                break;
            default:
                throw new ArgumentException("Passed GameState not supported");
        }
    }

    void FixedUpdate()
    {
        if (gameState == null)
        {
            throw new Exception("Game manager can't be null");
        }

        if (gameState.CurrentState == GameState.State.playing)
        {
            ActiveFixedUpdate();
        }
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameResumed += OnGameResumed;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameResumed -= OnGameResumed;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    protected virtual void ActiveUpdate() { }
    protected virtual void ActiveFixedUpdate() { }
    protected virtual void PausedUpdate() { }
    protected virtual void OnGameStarted() { }
    protected virtual void OnGameResumed() { }
    protected virtual void OnGameOverConfirmed() { }
}
