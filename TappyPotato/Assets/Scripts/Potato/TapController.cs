using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using TappyPotato.ScriptableObjects;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : BaseTappyController
{
    public delegate void PlayerDelegate();

    public float tapForce = 10;
    public float tiltSmooth = 5;

    public Vector3 startPos;
    public GameObject hayforks;

    private new Rigidbody2D rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation;

    private GameManager game;

    private Animator potatoAnimator;

    private float shiftSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        potatoAnimator = GetComponent<Animator>();
        downRotation = Quaternion.Euler(0, 0, -40);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
        rigidbody.simulated = false;
        shiftSpeed = hayforks.GetComponent<Parallaxer>().shiftSpeed;
    }

    protected override void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
        potatoAnimator.SetBool(PotatoState.PausedId, false);
    }

    protected override void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        potatoAnimator.SetBool(PotatoState.IsAliveId, true);
        potatoAnimator.SetBool(PotatoState.PausedId, true);
    }

    protected override void OnGameResumed()
    {
        rigidbody.simulated = true;
    }

    protected override void ActiveUpdate()
    {
        if (InputManager.Jump())
        {
            transform.rotation = forwardRotation;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(Vector2.up * tapForce, ForceMode2D.Force);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation,
            tiltSmooth * Time.deltaTime);

        potatoAnimator.SetBool(PotatoState.IsDiveId, transform.rotation.z < -0.07);

        GameManager.Instance.PositionX += shiftSpeed * Time.deltaTime;
    }

    protected override void PausedUpdate()
    {
        if (rigidbody.simulated)
        {
            rigidbody.simulated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.Score())
        {
            GameManager.Instance.PlayerScored();
        }

        if (collider.AnyDeath())
        {
            GameManager.Instance.PlayerDied();
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);
        }

        if (collider.DieAndStop() || collider.DieAndSlide())
        {
            rigidbody.simulated = false;
        }
    }
}
