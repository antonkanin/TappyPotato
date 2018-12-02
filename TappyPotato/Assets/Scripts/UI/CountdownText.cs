using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class CountdownText : MonoBehaviour
{
    public CountdownText()
    {
        Count = 3;
    }

    public int Count { get; set; }
    
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountdownFinished;

    private Text countdown;

    void OnEnable()
    {
        countdown = GetComponent<Text>();
        countdown.text = Count.ToString();
        StartCoroutine("Countdown");
    }

    IEnumerator Countdown()
    {
        int count = Count;
        for (int i = 0; i < count; i++)
        {
            countdown.text = (count - i).ToString();
            yield return new WaitForSeconds(1);
        }

        OnCountdownFinished();
    }
}
