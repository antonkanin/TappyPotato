using System.Collections;
using System.Collections.Generic;
using TappyPotato.ScriptableObjects;
using UnityEngine;

public class GraveSelector : MonoBehaviour
{
	[SerializeField]
	private GraveSprites _graveSprites;
	
	// Use this for initialization
	void Start ()
	{
		if (_graveSprites.Sprites != null && _graveSprites.Sprites.Length == 8)
		{
			GetComponent<SpriteRenderer>().sprite = _graveSprites.Sprites[Random.Range(0, 7)];
		}
		
	}
}
