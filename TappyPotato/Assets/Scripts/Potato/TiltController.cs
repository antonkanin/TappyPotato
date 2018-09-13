using UnityEngine;

public class TiltController : BaseTappyController
{
    const float tiltSpeed = 100.0f;
    private Quaternion downRotation;
    private Rigidbody2D rigidbody;

    void Start()
    {
        downRotation = Quaternion.Euler(0, 0, -40);
        rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void ActiveFixedUpdate()
    {
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, downRotation, tiltSpeed * Time.deltaTime);
        rigidbody.MoveRotation(rotation.eulerAngles.z);
    }
}
