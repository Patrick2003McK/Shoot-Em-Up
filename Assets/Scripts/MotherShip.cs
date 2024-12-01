using UnityEngine;

public class Mothership : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the mothership
    public int bonusPoints = 300; // Bonus points for destroying the mothership

    private Vector3 _direction = Vector3.right; // Movement direction

    private void Update()
    {
        // Move the mothership
        transform.position += _direction * speed * Time.deltaTime;

        // Check if the mothership is out of bounds
        Vector3 screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if (transform.position.x > screenBounds.x + 1 || transform.position.x < -screenBounds.x - 1)
        {
            Destroy(gameObject); // Despawn when off-screen
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            // Award bonus points and destroy the mothership
            ScoreManager.Instance.AddScore(300);
            Destroy(gameObject);
        }
    }
}
