using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager
{
    public static bool Jump()
    {
        return Input.GetMouseButtonDown(0);
    }
}
