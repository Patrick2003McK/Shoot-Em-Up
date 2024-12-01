using UnityEngine;

public class Bunker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the bunker is hit by an invader or missile
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader") ||
            other.gameObject.layer == LayerMask.NameToLayer("Missile"))
        {
            // Deactivate the bunker on impact
            this.gameObject.SetActive(false);
        }
    }
}
