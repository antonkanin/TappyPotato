using UnityEngine;

namespace TappyPotato.ScriptableObjects
{
    [CreateAssetMenu]
    public class GraveSprites : ScriptableObject
    {
        private Sprite[] _sprites;

        public Sprite[] Sprites
        {
            get
            {
                if (_sprites == null || _sprites.Length == 0)
                {
                    var graveSprites = Resources.LoadAll<Sprite>("rip/rip_sprite");
                    if (graveSprites != null && graveSprites.Length > 0)
                    {
                        _sprites = graveSprites;
                    }
                }

                return _sprites;
            }
        }
    }
}