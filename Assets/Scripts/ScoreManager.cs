using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Constants;

namespace ScoreUtils
{
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
    }
}