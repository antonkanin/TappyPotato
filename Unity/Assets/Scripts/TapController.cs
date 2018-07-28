using System.Collections;
using System.Collections.Generic;
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
    }

    void OnGameOverConfirmed()
    {
        transform.localPosition = startPos;
        transform.rotation = Quaternion.identity;
        potatoAnimator.SetBool("isAlive", true);
    }

    void Update()
    {
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

        if (collider.gameObject.tag == "DeadZone")
        {
            rigidbody.simulated = false;
            GameManager.Instance.PlayerDied();
            potatoAnimator.SetBool("isAlive", false);
            // play a sound
            dieAudio.Play();
        }
    }

}
