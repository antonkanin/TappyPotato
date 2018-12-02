using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public State CurrentState
    {
        get { return currentState; }
        set
        {
            if (currentState != value)
            {
                currentState = value;    
            }
        }
    }

    [SerializeField]
    private State currentState = State.notPlaying;

    public enum State
    {
        notPlaying,
        playing,
        paused
    };
}

