using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : BaseTappyController
{
    public bool IsRotating { get; set; }
    // Use this for initialization

    void Update()
    {
        if (IsRotating)
        {
            const float rotationSpeed = 2.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.right), rotationSpeed * Time.deltaTime);

            const float almostOne = 0.999f;
            if (Quaternion.Dot(transform.rotation, Quaternion.Euler(Vector3.right)) > almostOne)
            {
                IsRotating = false;
            }
        }
    }

    protected override void OnGameOverConfirmed()
    {
        IsRotating = false;
    }
}
