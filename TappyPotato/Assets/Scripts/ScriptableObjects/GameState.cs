using UnityEngine;

[CreateAssetMenu]
public class GameState : ScriptableObject
{
    public State state = State.notPlaying;

    public enum State
    {
        notPlaying,
        playing,
        paused
    };
}

