using System;
using System.Collections;
using UnityEngine;

public class RotationController : BaseTappyController
{
    public enum ERotationDirection
    {
        Horizontal,
        Vertical
    }

    private ERotationDirection rotationDirection = ERotationDirection.Horizontal;

    private Vector3 directionVector;

    public ERotationDirection RotationDirection
    {
        set
        {
            rotationDirection = value;
            directionVector = GetDirectionVector(value);
        }
    }

    private void Start()
    {
        RotationDirection = ERotationDirection.Horizontal;
    }

    private bool isRotating;

    void Update()
    {
        if (isRotating)
        {
            const float rotationSpeed = 2.0f;
            transform.rotation =
                Quaternion.Slerp(transform.rotation,
                    Quaternion.Euler(directionVector), rotationSpeed * Time.deltaTime);

            const float almostOne = 0.999f;
            if (Quaternion.Dot(transform.rotation, Quaternion.Euler(directionVector)) > almostOne)
            {
                isRotating = false;
            }
        }
    }

    private Vector3 GetDirectionVector(ERotationDirection direction)
    {
        if (direction == ERotationDirection.Horizontal)
        {
            return new Vector3(0, 0, 0);
        }

        return new Vector3(0, 0, 90);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.DieAndLooseEye())
        {
            StartCoroutine(Co_EnableRotation());
        }
    }

    private IEnumerator Co_EnableRotation()
    {
        yield return new WaitForSeconds(1.2f);

        isRotating = true;
    }

    protected override void OnGameOverConfirmed()
    {
        isRotating = false;
    }
}