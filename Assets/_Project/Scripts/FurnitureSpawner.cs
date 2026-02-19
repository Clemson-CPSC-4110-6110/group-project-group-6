using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Transform spawnPoint; // Where furniture appears
    public float spawnDistance = 0.5f; // Distance in front of spawn point
    public float spawnHeightOffset = 0f; // Height adjustment

    [Header("Furniture Prefabs")]
    public GameObject furnitureProto1;
    public GameObject furnitureProto2;

    [Header("Spawn Behavior")]
    public bool limitOneAtATime = false; // If true, only one furniture can exist at spawn area

    private GameObject lastSpawnedObject;

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("FurnitureSpawner: Spawn Point not assigned!");
        }
    }

    // Called by UI buttons
    public void proto1()
    {
        SpawnFurniture(furnitureProto1);
    }

    public void proto2()
    {
        SpawnFurniture(furnitureProto2);
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

        // Delete previous furniture if limit is enabled
        if (limitOneAtATime && lastSpawnedObject != null)
        {
            Destroy(lastSpawnedObject);
        }

        // Calculate spawn position
        Vector3 spawnPosition = spawnPoint.position +
                               (spawnPoint.forward * spawnDistance) +
                               (Vector3.up * spawnHeightOffset);

        // Spawn the furniture
        GameObject newFurniture = Instantiate(prefab, spawnPosition, spawnPoint.rotation);

        // Make sure gravity is off initially
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