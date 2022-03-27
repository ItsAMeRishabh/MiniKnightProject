using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class pool
{
    public string poolName;
    public int poolSize;
    public GameObject poolPrefab;
}

public class ObjectPooler : MonoBehaviour
{
    public List<pool> poolObjects = new List<pool>();
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public static ObjectPooler instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (pool item in poolObjects)
        {
            Queue<GameObject> objectsinQPool = new Queue<GameObject>();
            for (int i = 0; i < item.poolSize; i++)
            {
                GameObject temp = Instantiate(item.poolPrefab);
                temp.SetActive(false);
                objectsinQPool.Enqueue(temp);
            }
            poolDictionary.Add(item.poolName, objectsinQPool);
        }
    }

    public GameObject SpawnFromPool(string poolName, Vector3 position, Quaternion rotation)
    {
        GameObject temp = poolDictionary[poolName].Dequeue();
        temp.SetActive(true);
        temp.transform.position = position;
        temp.transform.rotation = rotation;
        poolDictionary[poolName].Enqueue(temp);
        return temp;
    }
}