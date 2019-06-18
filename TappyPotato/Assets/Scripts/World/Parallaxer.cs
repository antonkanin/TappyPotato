using UnityEngine;

public class Parallaxer : BaseTappyController {

    [System.Serializable]
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public float shiftSpeed;
    public float spawnDistance;
    public float removePositionX;

    public YSpawnRange ySpawnRange;

    public Vector3 defaultSpawnPos;

    public Vector3 immediateSpawnPos;
    public Vector2 targetAspectRatio;

    private float spawnTimer;
    private float targetAspect;

    private Transform[] poolObjects;
    private float mostLeftXPosition;

    void Awake()
    {
        mostLeftXPosition = 0;
        Initialize();
        Configure();
    }

    protected override void OnGameOverConfirmed()
    {
        mostLeftXPosition = 0;
        Configure();
    }

    protected override void ActiveUpdate()
    {
        Shift();
    }

    void Initialize()
    {
        poolObjects = new Transform[poolSize];
        for (int i = 0; i < poolObjects.Length; i++)
        {
            GameObject go = Instantiate(Prefab) as GameObject;
            poolObjects[i] = go.transform;
            poolObjects[i].SetParent(transform);
        }
    }

    void Configure()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            Vector3 pos = Vector3.zero;
            if (i == 0)
            {
                mostLeftXPosition = defaultSpawnPos.x;
            }
            else
            {
                mostLeftXPosition += spawnDistance;
            }

            pos.x = mostLeftXPosition;
            pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);

            poolObjects[i].position = pos;
        }
    }

    void Shift()
    {
        mostLeftXPosition -= shiftSpeed * Time.deltaTime;
        for (int i = 0; i < poolObjects.Length; i++)
        {
            poolObjects[i].transform.position += -Vector3.right * shiftSpeed * Time.deltaTime;
            CheckDisposedObject(poolObjects[i]);
        }
    }

    void CheckDisposedObject(Transform poolObject)
    {
        if (poolObject.transform.position.x < removePositionX)
        {
            Vector3 pos = Vector3.zero;
            mostLeftXPosition += spawnDistance;
            pos.x = mostLeftXPosition;
            pos.y = Random.Range(ySpawnRange.min, ySpawnRange.max);
            
            poolObject.transform.position = pos;
        }
    }
}