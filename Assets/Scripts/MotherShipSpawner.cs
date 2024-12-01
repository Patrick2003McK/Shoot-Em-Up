using UnityEngine;

public class MothershipSpawner : MonoBehaviour
{
    // Prefab of the mothership to spawn
    public Mothership mothershipPrefab;

    // Interval between mothership spawns
    public float spawnInterval = 20.0f;

    // Height at which the mothership spawns
    public float spawnHeight = 10.0f;

    private void Start()
    {
        // Repeatedly spawn a mothership at a regular interval
        InvokeRepeating(nameof(SpawnMothership), spawnInterval, spawnInterval);
    }

    private void SpawnMothership()
    {
        // Determine the spawn position (off-screen on the left)
        Vector3 spawnPosition = new Vector3(-14, 12, 0);

        // Instantiate the mothership and set its properties
        Mothership mothership = Instantiate(mothershipPrefab, spawnPosition, Quaternion.identity);
        mothership.transform.position = spawnPosition;

        // Ensure the mothership moves in the correct direction
        mothership.GetComponent<Mothership>().speed = Mathf.Abs(mothership.speed);
    }
}
