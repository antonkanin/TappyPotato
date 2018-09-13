using UnityEngine;

public class AudioController : BaseTappyController
{
    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    protected override void ActiveUpdate()
    {
        if (InputManager.Jump())
        {
            tapAudio.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.Score())
        {
            scoreAudio.Play();
        }

        if (collider.AnyDeath())
        {
            dieAudio.Play();
        }
    }
}

