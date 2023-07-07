using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int maxObjects = 10;

    [HideInInspector] public int objectCount;

    private GameManager gameManager;

    private void Start()
    {
        objectCount = 0;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Método para spawnear objetos
    public void SpawnObject(int count)
    {
        int availableSlots = maxObjects - objectCount;
        int spawnCount = Mathf.Min(count, maxObjects - objectCount);

        for (int i = 0; i < spawnCount; i++)
        {
            if (objectCount >= maxObjects)
                break;

            Transform spawnPoint = transform;
            GameObject spawnedObject = Instantiate(gameObject, spawnPoint.position, spawnPoint.rotation);
            spawnedObject.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 5f;
            Destroy(spawnedObject, 5f);

            objectCount++;
        }
    }

    // Método para spawnear objetos con un objeto específico
    public void SpawnObjectWithSpecificObj(int count, GameObject specificObject)
    {
        int availableSlots = maxObjects - objectCount;
        int spawnCount = Mathf.Min(count, maxObjects - objectCount);

        for (int i = 0; i < spawnCount; i++)
        {
            if (objectCount >= maxObjects)
                break;

            Transform spawnPoint = transform;
            GameObject spawnedObject = Instantiate(specificObject, spawnPoint.position, spawnPoint.rotation);
            spawnedObject.GetComponent<Rigidbody>().velocity = spawnPoint.forward * 5f;
            Destroy(spawnedObject, 5f);

            objectCount++;
        }
    }

    public void SubscribeToEvents(GameManager gameManager)
    {
        gameManager.AllSpawnEvent += SpawnObject;
        gameManager.OddsSpawnEvent += SpawnObject;
        gameManager.AllSpawnThisObjEvent += SpawnObjectWithSpecificObj;
    }
}
