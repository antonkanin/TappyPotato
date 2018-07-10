using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotatoSpriteController : MonoBehaviour
{

    public Sprite goingUp;

    public Sprite goingStraight;

    public Sprite goingDown;

    private enum Direction
    {
        Up,
        Straight,
        Down
    };

    private Direction direction_ = Direction.Straight;
    private SpriteRenderer spriteRenderer_;

	// Use this for initialization
	void Start ()
	{
	    spriteRenderer_ = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Direction currentDirection = GetDirection();
        Debug.Log(transform.localEulerAngles.z);
	    if (currentDirection != direction_)
	    {
	        direction_ = currentDirection;
	        UpdateSprite(direction_);
	    }
	}

    private void UpdateSprite(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                spriteRenderer_.sprite = goingUp;
                break;
            case Direction.Straight:
                spriteRenderer_.sprite = goingStraight;
                break;
            case Direction.Down:
                spriteRenderer_.sprite = goingDown;
                break;
            default:
                throw new ArgumentOutOfRangeException("direction", direction, null);
        }

    }

    private Direction GetDirection()
    {
        
        
        if (transform.eulerAngles.z > 10 && transform.eulerAngles.z <= 100)
        {
            return Direction.Up;
        }

        if (transform.eulerAngles.z <= 330 && transform.eulerAngles.z >= 270)
        {
            //Debug.Log("going down");
            return Direction.Down;
        }

        return Direction.Straight;
    }
}
