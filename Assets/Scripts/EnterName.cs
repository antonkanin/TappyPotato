using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class EnterName : MonoBehaviour
{
    public GameObject enterScoreDialog;

    void OnEnable()
    {
        // ask a player to enter a name if he didn't do it before
        if (!PlayerPrefs.HasKey(Const.PLAYER_NAME_PREF))
        {
            enterScoreDialog.SetActive(true);
        }
    }

}
