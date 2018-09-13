using System;
using UnityEngine;

public class HorizontalPositionContoller : BaseTappyController
{
    public FloatVariable horizontalPosition;
    public FloatVariable hayforksMovingSpeed;

    protected override void ActiveUpdate()
    {
        if (horizontalPosition != null)
        {
            horizontalPosition.Value += hayforksMovingSpeed.Value * Time.deltaTime;
        }
        else
        {
            throw new Exception("HorizontalPoision needs to be initialized");
        }
    }
}
