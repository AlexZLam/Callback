
using UnityEngine;

public class HazardController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore trigger colliders such as GroundCheck.
        if (other.isTrigger)
            return;

        // Only the player is affected by hazards.
        if (!other.CompareTag("Player"))
            return;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.Die();
        }
        else
        {
            Debug.LogError("No LevelManager found in the scene!");
        }
    }
}
