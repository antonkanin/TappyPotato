using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalPositionContoller : BaseTappyController
{
    public GameObject hayforks;
    private float shiftSpeed;

    void Start()
    {
        shiftSpeed = hayforks.GetComponent<Parallaxer>().shiftSpeed;
    }

    protected override void ActiveUpdate()
    {
        GameManager.Instance.PositionX += shiftSpeed * Time.deltaTime;
    }
}
