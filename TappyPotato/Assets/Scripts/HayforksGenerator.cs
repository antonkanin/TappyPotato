﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HayforksGenerator : MonoBehaviour
{

	public List<GameObject> hayforks;
	public float YOffset; 

	private void Awake()
	{
		var count = hayforks.Count;
		var index = Random.Range(0, count);

		var topFork = Instantiate(hayforks[index], transform);
		var topForkTransform = topFork.transform;
		topForkTransform.localPosition = new Vector3(0, YOffset, 0);
		topForkTransform.localScale = new Vector3(topForkTransform.localScale.x, -1 * topForkTransform.localScale.y, topForkTransform.localScale.z);

		index = Random.Range(0, count);
		var bottomFork = Instantiate(hayforks[index], transform);
		bottomFork.transform.localPosition = new Vector3(0, -YOffset, 0);
	}
}
