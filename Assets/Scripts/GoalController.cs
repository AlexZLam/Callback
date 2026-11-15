
using UnityEngine;

public class GoalController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore trigger colliders such as GroundCheck.
        if (other.isTrigger)
            return;

        // Only the player can complete the level.
        if (!other.CompareTag("Player"))
            return;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.WinLevel();
        }
        else
        {
            Debug.LogError("No LevelManager found in the scene!");
        }
    }
}
