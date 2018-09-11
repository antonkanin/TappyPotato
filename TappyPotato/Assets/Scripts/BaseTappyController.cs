using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTappyController : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance != null)
        {
            switch (GameManager.Instance.State)
            {
                case GameUIState.Playing:
                    ActiveUpdate();
                    break;
                case GameUIState.GamePaused:
                    PausedUpdate();
                    break;
                case GameUIState.GameOver:
                case GameUIState.Countdown:
                case GameUIState.Start:
                    break;
                default:
                    throw new ArgumentException("Passed GameUIState not supported");
            }
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.State == GameUIState.Playing)
            {
                ActiveFixedUpdate();
            }
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

    protected virtual void ActiveUpdate() {}
    protected virtual void ActiveFixedUpdate() {}
    protected virtual void PausedUpdate() {}
    protected virtual void OnGameStarted() {}
    protected virtual void OnGameResumed() { }
    protected virtual void OnGameOverConfirmed() {}
}
