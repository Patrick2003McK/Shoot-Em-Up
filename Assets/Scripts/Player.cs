using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Laser projectile prefab
    public Projectile laserPreFab;

    // Player's movement speed
    public float speed = 5.0f;

    // Fire rate in shots per second
    public float fireRate = 5.0f;
    private float _nextFireTime = 0f;

    // Screen boundaries
    private float _screenLeft;
    private float _screenRight;

    private void Start()
    {
        // Calculate screen boundaries in world coordinates
        float screenDistance = Camera.main.transform.position.z; // Camera is assumed to be orthographic
        _screenLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, -screenDistance)).x;
        _screenRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, -screenDistance)).x;
    }

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

        // Wrap player position at screen edges
        WrapPosition();

        // Handle shooting
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= _nextFireTime)
        {
            Shoot();
            _nextFireTime = Time.time + 1f / fireRate; // Set the next fire time based on the fire rate
        }
    }

    private void WrapPosition()
    {
        Vector3 position = this.transform.position;

        if (position.x < _screenLeft)
        {
            position.x = _screenRight;
        }
        else if (position.x > _screenRight)
        {
            position.x = _screenLeft;
        }

        this.transform.position = position;
    }

    private void Shoot()
    {
        // Instantiate a laser
        Instantiate(this.laserPreFab, this.transform.position, Quaternion.identity);
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
