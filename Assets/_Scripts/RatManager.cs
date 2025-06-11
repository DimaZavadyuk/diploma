using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RatManager : MonoBehaviour
{
    public GameObject ratPrefab; // Prefab for the rat
    public Collider spawnArea;   // Collider defining the spawn area
    public UnityEvent onAllRatsDestroyed; // Event to call when all rats are destroyed
    private bool wasStarted = false;
    private List<GameObject> activeRats = new List<GameObject>();

    // Spawns a specified number of rats within the collider area
    public void SpawnRats(int count)
    {
        wasStarted = true;
        if (ratPrefab == null || spawnArea == null) return;

        // Calculate the bounds of the spawn area
        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition = GetRandomPointInBounds(bounds);
            GameObject rat = Instantiate(ratPrefab, spawnPosition, Quaternion.identity);
            activeRats.Add(rat);
        }
    }

    // Checks if all rats have been destroyed and invokes the event
    private void Update()
    {
        if(!wasStarted) return;
        activeRats.RemoveAll(rat => rat == null);

        // If no active rats remain, invoke the event
        if (activeRats.Count == 0)
        {
            onAllRatsDestroyed.Invoke();
        }
    }

    // Gets a random point within the specified bounds
    private Vector3 GetRandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}