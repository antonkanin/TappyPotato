﻿using UnityEngine;

public class SlidingController : BaseTappyController
{
    private float slideDistance;
    private const float slideMaxDistance = 0.7f;
    private bool isSliding = false;

	void Update ()
	{
	    if (isSliding)
	    {
	        float slidingSpeed = 0.3f;
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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.DieAndSlide())
        {
            isSliding = true;
        }
    }

    protected override void OnGameStarted()
    {
        isSliding = false;
        slideDistance = 0;
    }

    protected override void OnGameOverConfirmed()
    {
        isSliding = false;
    }
}
