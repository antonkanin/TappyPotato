using UnityEngine;

public class RotationController : BaseTappyController
{
    private bool isRotating;

    void Update()
    {
        if (isRotating)
        {
            const float rotationSpeed = 2.0f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(Vector3.right), rotationSpeed * Time.deltaTime);

            const float almostOne = 0.999f;
            if (Quaternion.Dot(transform.rotation, Quaternion.Euler(Vector3.right)) > almostOne)
            {
                isRotating = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.AnyDeath())
        {
            isRotating = true;
        }
    }

    protected override void OnGameOverConfirmed()
    {
        isRotating = false;
    }
}
