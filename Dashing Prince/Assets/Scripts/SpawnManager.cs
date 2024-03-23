using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    public GameObject spawn; // The prefab to instantiate
    private GameObject spawnedObject; // Reference to the currently spawned object

    private void Start()
    {
        // Start the spawning coroutine
        StartCoroutine(SpawnObjectCoroutine());
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        while (true) 
        {
            yield return new WaitForSeconds(5f);

            // Check if the previous object has been destroyed
            if (spawnedObject == null)
            {
                Debug.Log("Enemy Spawned");

                // Instantiate a new object
                spawnedObject = Instantiate(spawn, transform.position, Quaternion.identity);
            }
        }
    }
}
