using UnityEngine;

namespace TappyPotato.ScriptableObjects
{
    [CreateAssetMenu]
    public class PotatoParameters : ScriptableObject
    {
        private Rigidbody2D rigidBody;

        private bool isSimulated;
        public bool IsSumulated
        {
            get { return isSimulated; }
            set
            {
                if (value != isSimulated)
                {
                    rigidBody.simulated = value;
                    isSimulated = value;
                }
            }
        }

        void Awake()
        {
            rigidBody = FindObjectOfType<TapController>().gameObject.GetComponent<Rigidbody2D>();
        }
    }
}