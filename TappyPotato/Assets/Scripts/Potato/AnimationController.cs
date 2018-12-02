using DefaultNamespace;
using UnityEngine;
using UnityEngine.Playables;

public class AnimationController : BaseTappyController
{
    private Animator potatoAnimator;

    void Start ()
    {
        potatoAnimator = GetComponent<Animator>();
    }

    protected override void OnGameStarted()
    {
        potatoAnimator.SetBool(PotatoState.PausedId, false);
    }

    protected override void OnGameOverConfirmed()
    {
        potatoAnimator.SetBool(PotatoState.IsAliveId, true);
        potatoAnimator.SetBool(PotatoState.PausedId, true);
    }

    protected override void ActiveUpdate()
    {
        potatoAnimator.SetBool(PotatoState.IsDiveId, transform.rotation.z < -0.07);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.AnyDeath())
        {
            //potatoAnimator.SetBool(PotatoState.IsAliveId, false);
            var rigidbody2 = GetComponent<Rigidbody2D>();
            rigidbody2.AddForce(new Vector2(-2f, 6f), ForceMode2D.Impulse);
            rigidbody2.AddTorque(1f, ForceMode2D.Impulse);
            var director = gameObject.GetComponent<PlayableDirector>();
            director.Play();
        }
    }
}
