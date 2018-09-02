using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingController : MonoBehaviour {

    private bool isSliding;
    private float slideDistance;
    private const float slideMaxDistance = 0.7f;

    // Use this for initialization
    void Start ()
    {
        isSliding = false;
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
}
