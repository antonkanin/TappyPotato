using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementController : BaseTappyController
{
    public float tapForce = 10;
    
    public Vector3 startPos;

    private new Rigidbody2D rigidbody;
    private Quaternion forwardRotation;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();

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
        if (collider.DieAndStop() || collider.DieAndSlide())
        {
            rigidbody.simulated = false;
        }
    }
}
