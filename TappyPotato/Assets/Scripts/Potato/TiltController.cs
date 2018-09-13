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
        transform.rotation = Quaternion.Slerp(transform.rotation, downRotation, tiltSmooth * Time.smoothDeltaTime);

        //transform.rotation = Quaternion.RotateTowards(transform.rotation, downRotation, tiltSmooth * Time.deltaTime);
    }
}
