using UnityEngine;

public class HayforksMovementDebug : MonoBehaviour
{
	private float delta;

	public GameState gameState;
	public GameObject hayforks;

	private void Awake()
	{
		delta = Mathf.Abs(transform.localPosition.x);
	}

	private void Update()
	{
		if (gameState.state == GameState.State.playing)
		{
			transform.Translate(Vector2.left * Time.deltaTime);

			var x = Mathf.Abs(transform.localPosition.x) - delta;
			if (x > 2)
			{
				delta = Mathf.Abs(transform.localPosition.x);
				var hayforksInstance = Instantiate(hayforks, transform);
				hayforksInstance.transform.localPosition = new Vector3(delta + 6, transform.localPosition.y, 10);
			}
		}
	}
}
