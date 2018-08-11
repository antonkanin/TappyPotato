using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TapController : MonoBehaviour
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

    private bool isSliding;
    private bool isRotating;
    private float slideDistance;
    private const float slideMaxDistance = 0.7f;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        potatoAnimator = GetComponent<Animator>();
        downRotation = Quaternion.Euler(0, 0, -40);
        forwardRotation = Quaternion.Euler(0, 0, 35);
        game = GameManager.Instance;
        rigidbody.simulated = false;
        isSliding = false;
        isRotating = false;

        shiftSpeed = hayforks.GetComponent<Parallaxer>().shiftSpeed;
    }

    void OnEnable()
    {
        GameManager.OnGameStarted += OnGameStarted;
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameStarted -= OnGameStarted;
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameStarted()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.simulated = true;
        isSliding = false;
        slideDistance = 0;
        potatoAnimator.SetBool(PotatoState.PausedId, false);
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        potatoAnimator.SetBool(PotatoState.IsAliveId, true);
        potatoAnimator.SetBool(PotatoState.PausedId, true);
        isSliding = false;
        isRotating = false;
    }

    void Update()
    {
        if (isSliding)
        {
            float slidingSpeed = 0.1f;
            slideDistance += slidingSpeed * Time.deltaTime;
            if (slideDistance > slideMaxDistance)
            {
                isSliding = false;
            }
            else
            {
                transform.position += Vector3.down * slidingSpeed * Time.deltaTime;
            }
        }

        if (isRotating)
        {
            const float rotationSpeed = 2.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.right), rotationSpeed * Time.deltaTime);

            const float almostOne = 0.999f;
            if (Quaternion.Dot(transform.rotation, Quaternion.Euler(Vector3.right)) > almostOne)
            {
                isRotating = false;
            }
        }

        if (game.GameOver)
        {
            return;
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
            isRotating = true;
            GameManager.Instance.PlayerDied();
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);
            // play a sound
            dieAudio.Play();

            if (collider.gameObject.CompareTag("DeadZoneSlide"))
            {
                isSliding = true;
            }
            else if (collider.gameObject.CompareTag("DeadZone")) 
            {
                rigidbody.simulated = true;
            }
        }
    }
}
