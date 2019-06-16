using DefaultNamespace;
using UnityEngine;

public class AnimationController : BaseTappyController
{
    private Animator potatoAnimator;

    private bool isPotatoAlive = true;

    void Start()
    {
        potatoAnimator = GetComponent<Animator>();
    }

    protected override void OnGameStarted()
    {
        potatoAnimator.SetBool(PotatoState.PausedId, false);
    }

    protected override void OnGameOverConfirmed()
    {
        isPotatoAlive = true;
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
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);

            if (isPotatoAlive)
            {
                if (collider.DieAndLooseEye())
                {
                    GetComponent<RotationController>().RotationDirection =
                        RotationController.ERotationDirection.Vertical;

                    Debug.Log("Loose eye animation. IsAlive " + isPotatoAlive);
                    potatoAnimator.SetInteger(PotatoState.DeathTypeId, DeathType.LooseEye);
                }
                else if (collider.DieAndSlide())
                {
                    Debug.Log("Bowell animation IsAlive " + isPotatoAlive);
                    potatoAnimator.SetInteger(PotatoState.DeathTypeId, DeathType.Bowell);
                }
            }

            isPotatoAlive = false;
        }
    }
}