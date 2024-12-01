using UnityEngine;

public class MothershipSpawner : MonoBehaviour
{
    public Mothership mothershipPrefab; // Assign the mothership prefab
    public float spawnInterval = 20.0f; // Time between spawns
    public float spawnHeight = 10.0f; // Height where the mothership spawns

    private void Start()
    {
        // Spawn motherships at regular intervals
        InvokeRepeating(nameof(SpawnMothership), spawnInterval, spawnInterval);
    }

    private void SpawnMothership()
    {
        Vector3 spawnPosition = new Vector3(-14, 12, 0); // Start from the left
        Mothership mothership = Instantiate(mothershipPrefab, spawnPosition, Quaternion.identity);

        // Ensure the mothership moves in the right direction
        mothership.transform.position = spawnPosition;
        mothership.GetComponent<Mothership>().speed = Mathf.Abs(mothership.speed);
    }
}
