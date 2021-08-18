using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    public List<Pool> pools;
    private int count;
 
    private void Start()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            PoolDictionary.Add(pool.tag, objectPool);
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpawnToPool("Circle");

        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            BackToPool("Circle");
        }
    }

    private GameObject SpawnToPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogError("That pool with tag " + tag + " doesnt exist.");
            return null;
        }

        count++;
        var objToSpawn = PoolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);

        PoolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn; 
    }

    private GameObject BackToPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.LogError("That pool with tag " + tag + " doesnt exist.");
            return null;
        }

        count--;
        var objToSpawn = PoolDictionary[tag].Dequeue();
        objToSpawn.SetActive(false);
        
        return objToSpawn; 
    }
    
    
}