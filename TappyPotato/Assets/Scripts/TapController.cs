using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : BaseTappyController
{
    public delegate void PlayerDelegate();

    public float tapForce = 10;
    public float tiltSmooth = 5;
    public Vector3 startPos;
    public GameObject hayforks;

    public AudioSource tapAudio;
    public AudioSource scoreAudio;
    public AudioSource dieAudio;

    private new Rigidbody2D rigidbody;
    private Quaternion downRotation;
    private Quaternion forwardRotation;

    private GameManager game;

    private Animator potatoAnimator;

    private float shiftSpeed;

    private RotationController rotationController;
    private SlidingController slidingController;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        slidingController = gameObject.GetComponent<SlidingController>();
        rotationController = gameObject.GetComponent<RotationController>();

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

    protected override void ActiveUpdate()
    {
        if (rigidbody.simulated == false)
        {
            rigidbody.simulated = true;
        }

        if (Input.GetMouseButtonDown(0))
        {
            tapAudio.Play();
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
        if (slidingController != null && slidingController.IsSliding == false)
        {
            rigidbody.simulated = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("ScoreZone"))
        {
            // register score event
            GameManager.Instance.PlayerScored();
            // play sound
            scoreAudio.Play();
        }

        if (collider.gameObject.CompareTag("DeadZone") || 
            collider.gameObject.CompareTag("DeadZoneSlide") ||
            collider.gameObject.CompareTag("DeadZoneGround")) 
        {
            rigidbody.simulated = false;
            rotationController.IsRotating = true;
            GameManager.Instance.PlayerDied();
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);
            // play a sound
            dieAudio.Play();

            if (collider.gameObject.CompareTag("DeadZoneSlide"))
            {
                slidingController.IsSliding = true;
            }
            else if (collider.gameObject.CompareTag("DeadZone")) 
            {
                rigidbody.simulated = true;
            }
        }
    }
}
