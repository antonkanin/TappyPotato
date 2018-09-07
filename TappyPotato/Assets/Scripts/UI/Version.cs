using UnityEngine;
using UnityEngine.UI;

public class Version : MonoBehaviour {
	private void Start()
	{
		var text = GetComponent<Text>();
		text.text = "Version: " + Application.version;
	}
}
