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

        private GameObject activeInstance;
        
        private void Start()
        {
            Application.targetFrameRate = 60;
            bool traceMotion = transform.childCount == 0;
            GetComponent<SpriteRenderer>().enabled = false;
            var screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            var currentPosition = Camera.main.ScreenToWorldPoint(new Vector3((GetSizeInPixels(transform).x / 2) + Screen.width, screenPosition.y));
            Debug.Log(currentPosition);

            var minDinstance = float.MaxValue;
            
            while (traceMotion)
            {
                var instance = Instantiate(prefab, transform);
                instance.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                instance.transform.localScale = new Vector3(1, 1, 1);
                instance.transform.position = currentPosition;                
                var screenPoint = Camera.main.WorldToScreenPoint(instance.transform.position);
                traceMotion = screenPoint.x > -GetSizeInPixels(instance.transform).x / 2;
                instance.isStatic = true;
                var dinstance = Mathf.Abs(transform.position.x - currentPosition.x);
                if (dinstance < minDinstance)
                {
                    minDinstance = dinstance;
                    if (activeInstance != null)
                    {
                        activeInstance.SetActive(false);                        
                    }
                    activeInstance = instance;
                }
                else
                {
                    instance.SetActive(false);
                }
                currentPosition = new Vector2(currentPosition.x - simStep.Value, currentPosition.y);
            }

            index = activeInstance.transform.GetSiblingIndex();

        }

        private int index = 0;
        private float time = 0;
        
        private void Update()
        {
            if (index >= transform.childCount)
            {
                index = 0;
                if (transform.localScale.y == -1)
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(-5f, -2.5f));
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, Random.Range(4f, 6.5f));
                }
                
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