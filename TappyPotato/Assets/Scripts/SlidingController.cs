using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingController : MonoBehaviour {

    //private bool isSliding;
    private float slideDistance;

    private TapController tapController;
    private const float slideMaxDistance = 0.7f;

    private bool isSliding = false;

    public bool IsSliding
    {
        get { return IsSliding; }
        set { isSliding = value; }
    }

    // Use this for initialization
    void Start ()
    {
    }
	
	// Update is called once per frame
	void Update ()
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
        isSliding = false;
        slideDistance = 0;
    }

    void OnGameOverConfirmed()
    {
        isSliding = false;
    }
}
