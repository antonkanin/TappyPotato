using UnityEngine;

public class DeathController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.AnyDeath())
        {
            GameManager.Instance.PlayerDied();
        }
    }
}
