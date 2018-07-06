using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Constants;
using UnityScript.Steps;

namespace ScoreUtils
{
    [Serializable]
    public class Player
    {
        public string player = "";
        public string Score = "";
    }

    [Serializable]
    class ScoreBoard
    {
        public List<Player> Items = null;
    }

    public class ScoreManager
    {
        public static IEnumerator SaveScore(string playerName, int score)
        {
            WWWForm form = new WWWForm();
            form.AddField(Const.NAME_FIELD, playerName);
            form.AddField(Const.SCORE_FIELD, score.ToString());

            UnityWebRequest request = UnityWebRequest.Post(Const.POST_URL, form);
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }

        public static IList<Player> GetScore()
        {
            var scoreBoardWww = Get(Const.GET_URL);
            string jsonResult = scoreBoardWww.text;

            Debug.Log(jsonResult);

            var scoreBoard = JsonUtility.FromJson<ScoreBoard>(jsonResult);
            return scoreBoard.Items;
        }

        private static WWW Get(string url)
        {
            WWW resultWww = new WWW(url);
            while (!resultWww.isDone)
            {
                WaitForSeconds wait = new WaitForSeconds(0.1f);
            }

            return resultWww;
        }
    }
}