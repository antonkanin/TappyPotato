using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTappyController : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.GamePlaying)
        {
            ActiveUpdate();
        }
        else
        {
            PausedUpdate();
        }
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    protected virtual void ActiveUpdate() {}
    protected virtual void PausedUpdate() {}
    protected virtual void OnGameStarted() {}
    protected virtual void OnGameOverConfirmed() {}
}
