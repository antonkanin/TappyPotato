using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugZAngle : MonoBehaviour {

	// Use this for initialization
    public Text textAngleZ;

	// Update is called once per frame
	void Update ()
	{
	    textAngleZ.text = transform.eulerAngles.z.ToString();
	}
}
