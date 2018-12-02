using UnityEngine;

namespace Potato
{
    public class AnimationEvent : MonoBehaviour
    {
        public GameObject _potato;
        
        public void OnAnimationEnd()
        {
            Debug.Log("Animation has been ended");
            /*var rigidbody = _potato.GetComponent<Rigidbody2D>();
            transform.position = _potato.transform.position;
            transform.rotation = _potato.transform.rotation;
            rigidbody.simulated = true;*/
        }
    }
}