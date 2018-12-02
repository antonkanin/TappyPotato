using UnityEngine;
using UnityEngine.Events;

public class DebugSceneController : MonoBehaviour
{
	public UnityEvent onSpaceBar;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			onSpaceBar.Invoke();
			
		}
	}

	public void Play()
	{
		Debug.LogFormat("Start Game");
		GameManager.Instance.StartGame();
		GameManager.Instance.countDownPage.GetComponent<CountdownText>().Count = 1000;
	}
}
