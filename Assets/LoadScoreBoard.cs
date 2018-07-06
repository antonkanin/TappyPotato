using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ScoreUtils;

public class LoadScoreBoard : MonoBehaviour
{
    public GameObject playerScorePrefab;

    // Use this for initialization
    void Start()
    {
        var scoreBoard = ScoreUtils.ScoreReader.GetScore();
        foreach (var playerScore in scoreBoard)
        {
            GameObject scoreLine = (GameObject) Instantiate(playerScorePrefab);
            scoreLine.transform.SetParent(this.transform, false);
            scoreLine.transform.Find("PlayerText").GetComponent<Text>().text = playerScore.player;
            scoreLine.transform.Find("ScoreText").GetComponent<Text>().text = playerScore.score;
        }
    }
}