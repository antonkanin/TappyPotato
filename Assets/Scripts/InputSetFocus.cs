﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputSetFocus : MonoBehaviour
{
    public InputField inputField;
	// Use this for initialization
	void Start ()
	{
		inputField.ActivateInputField();
	}
}
