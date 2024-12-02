using UnityEngine;

public class Invader : MonoBehaviour
{
    // Array of sprites used for the animation of the invader
    public Sprite[] animationSprites;

    // Time interval between animation frames
    public float animationTime = 1.0f;

    // Action to be invoked when the invader is killed
    public System.Action killed;

    // Reference to the SpriteRenderer component
    private SpriteRenderer _spriteRenderer;

    // Tracks the current frame of the animation
    private int _animationFrame;

    private void Awake()
    {
        // Get the SpriteRenderer component attached to this GameObject
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        // Repeatedly invoke the AnimateSprite method at intervals defined by animationTime
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    // Handles cycling through the sprite frames for animation
    private void AnimateSprite()
    {
        // Move to the next frame
        _animationFrame++;

        // Loop back to the first frame if the end of the array is reached
        if (_animationFrame >= this.animationSprites.Length)
        {
            _animationFrame = 0;
        }

        // Update the sprite to the current frame
        _spriteRenderer.sprite = this.animationSprites[_animationFrame];
    }

    // Detect collisions with other objects
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collision is with a laser (by layer)
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            // Invoke the killed action
            this.killed.Invoke();

            // Add points to the player's score
            ScoreManager.Instance.AddScore(50);

            // Deactivate this invader
            this.gameObject.SetActive(false);
        }
    }
}
