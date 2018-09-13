using UnityEngine;

public class ScoreController : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.Score())
        {
            GameManager.Instance.PlayerScored();
        }
    }
}
