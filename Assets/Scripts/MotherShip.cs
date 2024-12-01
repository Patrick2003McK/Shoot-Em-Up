using UnityEngine;

public class Mothership : MonoBehaviour
{
    // Speed at which the mothership moves across the screen
    public float speed = 5.0f;

    // Bonus points awarded when the mothership is destroyed
    public int bonusPoints = 300;

    // Direction of the mothership's movement
    private Vector3 _direction = Vector3.right;

    private void Update()
    {
        // Move the mothership in its current direction
        transform.position += _direction * speed * Time.deltaTime;

        // Destroy the mothership if it goes off-screen
        Vector3 screenBounds = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        if (transform.position.x > screenBounds.x + 1 || transform.position.x < -screenBounds.x - 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the mothership is hit by a laser
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            // Award bonus points and destroy the mothership
            ScoreManager.Instance.AddScore(bonusPoints);
            Destroy(gameObject);
        }
    }
}
