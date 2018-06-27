using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxer : MonoBehaviour {

    class PoolObject
    {
        public Transform transform;
        public bool inUse;

        public PoolObject(Transform t)
        {
            Transform = t;
        }

        public void Use()
        {
            inUse = true;
        }

        public void Dispose()
        {
            inUse = false;
        }
    }

    [System.Serializable]
    public struct YSpawnRange
    {
        public float min;
        public float max;
    }

    public GameObject Prefab;
    public int poolSize;
    public shiftSpeed;
    public float SpawnRate;

    public YSpawnRange YSpawnRange;

    public Vector3 defaultSpawnPos;
    public bool spawnImmediate;

    public Vector3 immediateSpawnPos;
    public Vector2 targetAspectRatio;

    private float spawnTimer;
    private float targetAspect;

    private PoolObject[] poolObjects;

    private GameManager game;

    void Awake()
    {

    }

    void Start()
    {
        game = GameManager.Instance;
    }

    void OnEnable()
    {
        GameManager.OnGameOverConfirmed += OnGameOverConfirmed;
    }

    void OnDisable()
    {
        GameManager.OnGameOverConfirmed -= OnGameOverConfirmed;
    }

    void OnGameOverConfirmed()
    {
        Configure();
    }

    void Update()
    {

    }

    void Configure()
    {

    }

    void Spawn()
    {

    }

    void SpawnImmediate()
    {

    }

    void Shift()
    {

    }

    void CheckDisposedObject(PoolObject poolObject)
    {

    }

    Transform GetPoolObject()
    {
        for (int i = 0; i < poolObjects.Length; i++)
        {
            if (!poolObjects[i].inUse)
            {
                poolObjects[i].Use();
                return poolObjects[i]
            }

            return null;
        }

    }
}


