using System;
using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;

public class DeadPotatoesManager : MonoBehaviour
{
    public GameObject deadPotatoPrefab;

	// Use this for initialization
	void Start () {
		Configure();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Configure()
    {
        ScoreManager.Instance.GetScoreAsync(LoadPlayers);
    }

    private void LoadPlayers(IList<Player> players)
    {
        foreach (var player in players)
        {
            Debug.Log("Death position : " + player.death_position);

            GameObject go = Instantiate(deadPotatoPrefab) as GameObject;
            Transform t = go.transform;
            t.SetParent(transform);

            float x = Convert.ToInt32(player.death_position) / 10;
            t.position.x = x;

        }
    }
}
