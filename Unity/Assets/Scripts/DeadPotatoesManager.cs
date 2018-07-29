using System;
using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;
using UnityEngine.UI;

public class DeadPotatoesManager : MonoBehaviour
{
    public GameObject deadPotatoPrefab;
    public GameObject hayForks; // we need this to get hayworks shift speed

    private IList<GameObject> deadPotatoesArray;
    private float shiftSpeed;

	void Start ()
	{
	    deadPotatoesArray = new List<GameObject>();
        shiftSpeed = hayForks.GetComponent<Parallaxer>().shiftSpeed;
        Configure();
	}
	
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
            player.transform.position -= new Vector3(shiftSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    private void OnGameOverConfirmed()
    {
        Configure();
    }

    private void Configure()
    {
        ScoreManager.Instance.GetScoreAsync(LoadPlayers);
    }

    private void LoadPlayers(IList<Player> players)
    {
        foreach (var deadPotato in deadPotatoesArray)
        {
            Destroy(deadPotato);
        }
        deadPotatoesArray.Clear();

        foreach (var player in players)
        {
            GameObject deadPotato = Instantiate(deadPotatoPrefab) as GameObject;
            Transform t = deadPotato.transform;
            t.Find("Canvas").Find("PlayerNameText").GetComponent<Text>().text = player.player_name;
            t.SetParent(transform);

            float x = Convert.ToInt32(player.death_position) / 10;
            t.position += new Vector3(x, 0.0f, 0.0f);
            deadPotatoesArray.Add(deadPotato);
        }
    }
}
