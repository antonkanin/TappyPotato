using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveSelector : MonoBehaviour
{

	private static Sprite[] _sprites;
	
	// Use this for initialization
	void Start ()
	{
		if (_sprites == null)
		{
			_sprites = Resources.LoadAll<Sprite>("rip/rip_sprite");
		}
		GetComponent<SpriteRenderer>().sprite = _sprites[Random.Range(0, 7)];
	}
}
