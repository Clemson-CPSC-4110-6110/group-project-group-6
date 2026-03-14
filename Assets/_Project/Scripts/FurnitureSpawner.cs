using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform spawnPoint;
    public float spawnDistance = 0.5f;
    public float spawnHeightOffset = 0f;

    [Header("Furniture Prefabs")]
    public GameObject fridge;
    public GameObject couch;
    public GameObject air_hockey;
    public GameObject coffee_table;
    public GameObject dish;
    public GameObject drink;
    public GameObject dumbbell;
    public GameObject flower;
    public GameObject lamp;
    public GameObject piano;
    public GameObject toy;
    public GameObject scratch_post;

    [Header("Spawn Behavior")]
    public bool limitOneAtATime = false;

    private GameObject lastSpawnedObject;

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("FurnitureSpawner: Spawn Point not assigned!");
        }
    }
    public void fridge_spawn()
    {
        SpawnFurniture(fridge);
    }

    public void couch_spawn()
    {
        SpawnFurniture(couch);
    }

    public void air_hockey_spawn()
    {
        SpawnFurniture(air_hockey);
    }
    public void coffee_table_spawn()
    {
        SpawnFurniture(coffee_table);
    }

    public void dish_spawn()
    {
        SpawnFurniture(dish);
    }

    public void drink_spawn()
    {
        SpawnFurniture(drink);
    }

    public void dumbbell_spawn()
    {
        SpawnFurniture(dumbbell);
    }

    public void flower_spawn()
    {
        SpawnFurniture(flower);
    }

    public void lamp_spawn()
    {
        SpawnFurniture(lamp);
    }

    public void piano_spawn()
    {
        SpawnFurniture(piano);
    }

    public void toy_spawn()
    {
        SpawnFurniture(toy);
    }

    public void scratch_spawn()
    {
        SpawnFurniture(scratch_post);
    }

    void SpawnFurniture(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("Furniture prefab is null!");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not set!");
            return;
        }

        if (limitOneAtATime && lastSpawnedObject != null)
        {
            Destroy(lastSpawnedObject);
        }

        Vector3 spawnPosition = spawnPoint.position +
                               (spawnPoint.forward * spawnDistance) +
                               (Vector3.up * spawnHeightOffset);

        GameObject newFurniture = Instantiate(prefab, spawnPosition, spawnPoint.rotation);

        Rigidbody rb = newFurniture.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
        }

        lastSpawnedObject = newFurniture;

        Debug.Log("Spawned: " + prefab.name + " at " + spawnPosition);
    }

    public void ClearAllFurniture()
    {
        GameObject[] allFurniture = GameObject.FindGameObjectsWithTag("Furniture");
        foreach (GameObject furniture in allFurniture)
        {
            Destroy(furniture);
        }

        lastSpawnedObject = null;
        Debug.Log("Cleared all furniture");
    }
}