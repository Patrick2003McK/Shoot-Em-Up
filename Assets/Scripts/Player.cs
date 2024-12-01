using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Laser projectile prefab
    public Projectile laserPreFab;

    // Player's movement speed
    public float speed = 5.0f;

    // Tracks whether a laser is currently active (to avoid multiple shots at once)
    private bool _laserActive;

    private void Update()
    {
        // Handle horizontal movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        // Handle shooting
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (!_laserActive)
        {
            // Instantiate a laser and attach its destruction callback
            Projectile projectile = Instantiate(this.laserPreFab, this.transform.position, Quaternion.identity);
            projectile.destroyed += LaserDestroyed;
            _laserActive = true;
        }
    }

    private void LaserDestroyed()
    {
        // Reset laser activity when the laser is destroyed
        _laserActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Restart the game if the player is hit by an invader or missile
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
