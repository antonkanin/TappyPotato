using UnityEngine;

public class HayforksMovementDebug : MonoBehaviour
{
	private bool inMotion = false;

	public GameState gameState;

	private void Update()
	{
		if (gameState.state == GameState.State.playing)
		{
			transform.Translate(Vector2.left * Time.deltaTime);			
		}
	}
}
