using System.Collections;
using System.Collections.Generic;
using PerformanceOptimization.Scripts.Assets;
using UnityEngine;

public class CameraRails : MonoBehaviour
{

	public FloatVariable MoveSpeed;

	private void Update () {
		if (transform.localPosition.x > 25)
		{
			transform.localPosition = Vector2.zero;
		}
		else
		{
			var nextX = MoveSpeed.Value * Time.deltaTime;
			gameObject.transform.Translate(nextX, 0, 0);
		}
	}

}
