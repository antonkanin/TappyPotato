using System.Collections;
using System.Collections.Generic;
using Constants;
using UnityEngine;

public static class ColliderExtensions
{
    public static bool Score(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag(TappyTag.ScoreZone);
    }

    public static bool DieAndLooseEye(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag(TappyTag.DeadZoneEye);
    }

    public static bool DieAndSlide(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag(TappyTag.DeadZoneSlide);
    }

    public static bool DieAndKeepFalling(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag(TappyTag.DeadZone);
    }

    public static bool DieAndStop(this Collider2D scoreCollider)
    {
        return scoreCollider.gameObject.CompareTag(TappyTag.DeadZoneGround);
    }

    public static bool AnyDeath(this Collider2D scoreCollider)
    {
        return
            scoreCollider.DieAndLooseEye() ||
            scoreCollider.DieAndSlide() ||
            scoreCollider.DieAndKeepFalling() ||
            scoreCollider.DieAndStop();
    }
}