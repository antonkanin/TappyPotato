using PerformanceOptimization.Scripts.Assets;
using UnityEngine;

namespace PerformanceOptimization.Scripts
{
    public class SimMotion : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private FloatVariable minDeltaTime;

        [SerializeField]
        private FloatVariable simStep;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            bool traceMotion = transform.childCount == 0;
            GetComponent<SpriteRenderer>().enabled = false;
            var parentPosition = transform.position;
            Debug.Log(parentPosition);
            
            while (traceMotion)
            {
                var instance = GameObject.Instantiate(prefab, transform);
                instance.transform.localScale = new Vector3(1, 1, 1);
                instance.transform.position = parentPosition;
                parentPosition = new Vector2(parentPosition.x - simStep.Value, parentPosition.y);
                var screenPoint = Camera.main.WorldToScreenPoint(instance.transform.position);
                traceMotion = screenPoint.x > -GetSizeInPixels(instance.transform).x / 2;
                instance.isStatic = true;
                instance.SetActive(false);
            }
        }

        private int index = 0;
        private float time = 0;
        
        private void Update()
        {
            if (index >= transform.childCount)
            {
                index = 0;
            }
            
            if (index == 0 || Time.time - time > minDeltaTime.Value)
            {
                transform.GetChild(index).gameObject.SetActive(true);
                if (index > 0)
                {
                    transform.GetChild(index - 1).gameObject.SetActive(false);
                }
                time = Time.time;
                index++;
            }
        }

        private Vector2 GetSizeInPixels(Transform transform)
        {
            var spriteRenderer = transform.GetComponent<SpriteRenderer>();
            Vector2 spriteSize = spriteRenderer.sprite.rect.size;
            Vector2 localSize = spriteSize / spriteRenderer.sprite.pixelsPerUnit;

            Vector3 worldSize = localSize;
            worldSize.x *= transform.lossyScale.x;
            worldSize.y *= transform.lossyScale.y;

            Vector3 screenSize = 0.5f * worldSize / Camera.main.orthographicSize;
            screenSize.y *= Camera.main.aspect;

            return new Vector3(screenSize.x * Camera.main.pixelWidth, screenSize.y * Camera.main.pixelHeight, 0) * 0.5f;
        }
    }
}