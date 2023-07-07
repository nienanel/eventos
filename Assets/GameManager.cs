using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public List<Spawner> spawners; 
    public GameObject[] spawnableObjects; 

    private float nextInterval; 
    private float counter; 

    public event Action<int> AllSpawnEvent; 
    public event Action<int> OddsSpawnEvent; 
    public event Action<int, GameObject> AllSpawnThisObjEvent; 

    private void Start()
    {
        SetNextInterval();

        foreach (Spawner spawner in spawners)
        {
            spawner.SubscribeToEvents(this);
        }
    }

    private void Update()
    {
        counter += Time.deltaTime; 
        
        if ( counter >= nextInterval)
        {
            counter = 0f;
            SpawnRandomType();
            SetNextInterval();
        }      
    }

    // Configura el siguiente intervalo aleatorio y restablece el contador
    private void SetNextInterval()
    {
        nextInterval = UnityEngine.Random.Range(1f, 5f);
    }

    private void SpawnRandomType()
    {
        int randomType = UnityEngine.Random.Range(0, 3); 

        // Spawnea un tipo de objeto en base al número aleatorio
        switch (randomType)
        {
            case 0:
                AllSpawnEvent?.Invoke(GetSpawnCount());
                break;
            case 1:
                OddsSpawnEvent?.Invoke(GetSpawnCount());
                break;
            case 2:
                GameObject randomObject = GetRandomSpawnableObject();
                AllSpawnThisObjEvent?.Invoke(GetSpawnCount(), randomObject);
                break;
        }
    }

    private int GetSpawnCount()
    {
        return UnityEngine.Random.Range(5, 11);
    }

    private GameObject GetRandomSpawnableObject()
    {
        return spawnableObjects[UnityEngine.Random.Range(0, spawnableObjects.Length)];
    }
}
