using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderExtensions
{
    public static bool Score(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag("ScoreZone");
    }

    public static bool DieAndSlide(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag("DeadZoneSlide");
    }

    public static bool DieAndKeepFalling(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag("DeadZone");
    }

    public static bool DieAndStop(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag("DeadZoneGround");
    }

    public static bool AnyDeath(this Collider2D scoreCollider)
    {
        return scoreCollider.DieAndSlide() || scoreCollider.DieAndKeepFalling() || scoreCollider.DieAndStop();
    }
}

