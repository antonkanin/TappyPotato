using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using TappyPotato.ScriptableObjects;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : BaseTappyController
{
    public float tapForce = 10;
    public float tiltSmooth = 5;

    public Vector3 startPos;

    private new Rigidbody2D rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

        downRotation = Quaternion.Euler(0, 0, -40);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        rigidbody.simulated = false;
    }

    protected override void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
    }

    protected override void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
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
        if (collider.AnyDeath())
        {
            GameManager.Instance.PlayerDied();
        }

        if (collider.DieAndStop() || collider.DieAndSlide())
        {
            rigidbody.simulated = false;
        }
    }
}
