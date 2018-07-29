using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScoreBoard : MonoBehaviour
{
    public GameObject playerScorePrefab;

    // Use this for initialization
    void OnEnable()
    {
        Debug.Log("ScoreBoard content panel enabled");
        if (GameManager.Instance.GotScoreFromServer)
        {
            var scoreBoard = GameManager.Instance.ScoreBoard;
            foreach (var playerScore in scoreBoard)
            {
                GameObject scoreLine = (GameObject)Instantiate(playerScorePrefab);
                scoreLine.transform.SetParent(this.transform, false);
                scoreLine.transform.Find("PlayerText").GetComponent<Text>().text = playerScore.player_name;
                scoreLine.transform.Find("ScoreText").GetComponent<Text>().text = playerScore.score;
            }
        }
        else
        {
            Debug.Log("Did not get score board from the GameManager");
        }
    }
}