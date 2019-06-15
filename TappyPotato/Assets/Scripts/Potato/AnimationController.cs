using DefaultNamespace;
using UnityEngine;

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
            Debug.Log("potatoAnimator.SetBool(PotatoState.IsAliveId, false);");
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);
            potatoAnimator.SetInteger(PotatoState.DeathTypeId, DeathType.Forks);
        }
    }
}
