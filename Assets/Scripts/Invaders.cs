using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour
{
    // Prefabs for invader rows
    public Invader[] prefabs;

    // Number of rows and columns of invaders
    public int rows = 5;
    public int columns = 11;

    // Controls the speed of the invaders based on how many are killed
    public AnimationCurve speed;

    // Prefab for missiles fired by the invaders
    public Projectile missilePrefab;

    // Time between missile attacks
    public float MissileAttackRate = 1.0f;

    // Number of invaders killed
    public int amountKilled { get; private set; }

    // Number of invaders still alive
    public int amountAlive => this.totalInvaders - this.amountKilled;

    // Total number of invaders
    public int totalInvaders => this.rows * this.columns;

    // Percentage of invaders killed
    public float percentKilled => (float)this.amountKilled / this.totalInvaders;

    // Current direction of invader movement
    private Vector3 _direction = Vector2.right;

    private void Awake()
    {
        // Spawn rows and columns of invaders
        for (int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

            for (int col = 0; col < this.columns; col++)
            {
                // Instantiate an invader from the prefab
                Invader invader = Instantiate(this.prefabs[row], this.transform);

                // Attach the killed event handler
                invader.killed += InvaderKilled;

                // Position the invader in the grid
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start()
    {
        // Start the missile attack loop
        InvokeRepeating(nameof(MissileAttack), this.MissileAttackRate, this.MissileAttackRate);
    }

    private void Update()
    {
        // Move invaders horizontally based on speed and direction
        this.transform.position += _direction * this.speed.Evaluate(this.percentKilled) * Time.deltaTime;

        // Check screen boundaries for collision
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;

            // Change direction when invaders hit the screen edge
            if (_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
                AdvanceRow();
            else if (_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
                AdvanceRow();
        }
    }

    private void AdvanceRow()
    {
        // Reverse direction and move the invaders down one row
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void MissileAttack()
    {
        // Each invader has a random chance to fire a missile
        foreach (Transform invader in this.transform)
        {
            if (!invader.gameObject.activeInHierarchy)
                continue;

            // Random chance based on the number of alive invaders
            if (Random.value < (1.0f / (float)this.amountAlive))
            {
                Instantiate(this.missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
    }

    private void InvaderKilled()
    {
        // Increment the kill count
        this.amountKilled++;

        // Reload the scene if all invaders are killed
        if (this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
