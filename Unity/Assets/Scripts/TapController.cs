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
            // https://www.youtube.com/watch?v=nJiFitClnKo
            float rotationSpeed = 0.01f;
            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.right, Vector3.right, step, 0.0f);

            //transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(newDirection);
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

        GameManager.Instance.PositionX += shiftSpeed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "ScoreZone")
        {
            // register score event
            GameManager.Instance.PlayerScored();
            // play sound
            scoreAudio.Play();
        }

        if (collider.gameObject.tag == "DeadZone" || collider.gameObject.tag == "DeadZoneSlide") 
        {
            rigidbody.simulated = false;
            isRotating = true;
            GameManager.Instance.PlayerDied();
            potatoAnimator.SetBool(PotatoState.IsAliveId, false);
            // play a sound
            dieAudio.Play();

            if (collider.gameObject.tag == "DeadZoneSlide")
            {
                isSliding = true;
            }
        }
    }
}
