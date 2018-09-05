using System;
using System.Collections;
using System.Collections.Generic;
using PlayerClasses;
using UnityEngine;
using UnityEngine.UI;

public class DeadPotatoesManager : BaseTappyController
{
    public GameObject deadPotatoPrefab;
    public GameObject hayForks; // we need this to get hayworks shift speed

    private IList<GameObject> deadPotatoesArray;
    private float shiftSpeed;

    private float potatoObjectX;

	void Start ()
	{
	    deadPotatoesArray = new List<GameObject>();
        shiftSpeed = hayForks.GetComponent<Parallaxer>().shiftSpeed;

	    var potatoGameObject = GameObject.Find("potato");
	    if (potatoGameObject == null)
	    {
            throw new System.Exception("potato game object not found");
	    }
	    potatoObjectX = potatoGameObject.transform.position.x;

        Configure();
	}
	
	protected override void ActiveUpdate()
	{
	    ShiftDeadPotatoes();   
	}

    void ShiftDeadPotatoes()
    {
        foreach (var player in deadPotatoesArray)
        {
            player.transform.position -= new Vector3(shiftSpeed * Time.deltaTime, 0.0f, 0.0f);
        }
    }

    protected override void OnGameOverConfirmed()
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
            //t.Find("Canvas").Find("PlayerNameText").GetComponent<Text>().text = player.player_name;
            t.SetParent(transform);

            float x = Convert.ToInt32(player.death_position) / 10.0f + potatoObjectX;
            t.position += new Vector3(x, 0.0f, 0.0f);
            deadPotatoesArray.Add(deadPotato);
        }
    }
}
