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
			var next = transform.localPosition + new Vector3(MoveSpeed.Value, 0, 0);
			gameObject.transform.localPosition = next;
		}
	}

}
