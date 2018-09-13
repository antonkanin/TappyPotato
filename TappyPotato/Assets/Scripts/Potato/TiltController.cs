using UnityEngine;

public class TiltController : BaseTappyController
{
    public float tiltSmooth = 5;
    private Quaternion downRotation;

    void Start()
    {
        downRotation = Quaternion.Euler(0, 0, -40);
    }

    protected override void ActiveFixedUpdate()
    {
        transform.Rotate(0, 0, -1.1f);
        //transform.rotation = Quaternion.Slerp(transform.rotation, downRotation, 0.05f);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }
}
