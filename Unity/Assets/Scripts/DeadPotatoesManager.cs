using System;
using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;
using UnityEngine.UI;

public class DeadPotatoesManager : MonoBehaviour
{
    public GameObject deadPotatoPrefab;
    public GameObject hayForks; // we need this to get document shift speed

    private IList<Transform> deadPotatoesArray;
    private float shiftSpeed;

	// Use this for initialization
	void Start ()
	{
	    deadPotatoesArray = new List<Transform>();
        shiftSpeed = hayForks.GetComponent<Parallaxer>().shiftSpeed;
        Configure();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (GameManager.Instance.GameOver)
	        return;

	    ShiftDeadPotatoes();   
	}

    void ShiftDeadPotatoes()
    {
        foreach (var player in deadPotatoesArray)
        {
            player.position -= new Vector3(shiftSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    private void Configure()
    {
        ScoreManager.Instance.GetScoreAsync(LoadPlayers);
    }

    private void LoadPlayers(IList<Player> players)
    {
        deadPotatoesArray.Clear();
        Debug.Log("Player array length: " + players.Count);
        foreach (var player in players)
        {
            //Debug.Log("Death position : " + player.death_position);

            GameObject deadPotato = Instantiate(deadPotatoPrefab) as GameObject;
            Transform t = deadPotato.transform;
            t.Find("Canvas").Find("PlayerNameText").GetComponent<Text>().text = player.player_name;
            //Debug.Log("Child count: " + deadPotato.transform.childCount);
            t.SetParent(transform);

            float x = Convert.ToInt32(player.death_position) / 10;
            t.position += new Vector3(x, 0.0f, 0.0f);
            deadPotatoesArray.Add(t);
        }
    }
}
