using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ScoreUtils
{
    public class ScoreManager
    {
        public static IEnumerator SaveScore(string playerName, int score)
        {
            WWWForm form = new WWWForm();
            form.AddField("name", playerName);
            form.AddField("score", score.ToString());

            UnityWebRequest request = UnityWebRequest.Post("http://localhost/score_post.php", form);
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}