using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterName : MonoBehaviour
{
    public GameObject enterScoreDialog;

    void OnEnable()
    {
        enterScoreDialog.SetActive(true);
    }

}
