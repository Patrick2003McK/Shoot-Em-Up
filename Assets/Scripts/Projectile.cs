using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Direction in which the projectile will move
    public Vector3 direction;

    // Speed at which the projectile travels
    public float speed;

    // Event triggered when the projectile is destroyed
    public System.Action destroyed;

    private void Update()
    {
        // Move the projectile in the specified direction
        this.transform.position += this.direction * this.speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Invoke the destroyed event if assigned
        if (this.destroyed != null)
        {
            this.destroyed.Invoke();
        }

        // Destroy the projectile when it hits something
        Destroy(this.gameObject);
    }
}
