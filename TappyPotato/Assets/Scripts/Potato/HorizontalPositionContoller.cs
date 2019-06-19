using System;
using UnityEngine;

public class HorizontalPositionContoller : BaseTappyController
{
    public FloatVariable horizontalPosition;

    public Parallaxer hayforksParallaxer;

    protected override void ActiveUpdate()
    {
        if (horizontalPosition == null)
        {
            throw new NullReferenceException("horizontalPosition needs to be initialized");
        }

        if (hayforksParallaxer == null)
        {
            throw new NullReferenceException("hayforksParallaxer needs to be initialized");
        }

        horizontalPosition.Value += hayforksParallaxer.shiftSpeed * Time.deltaTime;
    }
}