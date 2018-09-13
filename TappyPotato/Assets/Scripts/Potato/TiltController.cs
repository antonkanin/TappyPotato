using UnityEngine;

public class TiltController : BaseTappyController
{
    const float tiltSpeed = 100.0f;
    private Quaternion downRotation;
    private Rigidbody2D rb;

    void Start()
    {
        downRotation = Quaternion.Euler(0, 0, -40);
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void ActiveFixedUpdate()
    {
        Quaternion rot = Quaternion.RotateTowards(transform.rotation, downRotation, tiltSpeed * Time.deltaTime);
        rb.MoveRotation(rot.eulerAngles.z);
    }
}
