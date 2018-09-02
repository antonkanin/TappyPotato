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

    protected abstract void ActiveUpdate();
    protected abstract void PausedUpdate();
}
