
using UnityEngine;

public class LeverController : SwitchController
{
    public bool playerInRange;

    void Update()
    {
        if (LevelManager.Instance != null &&
            !LevelManager.Instance.IsPlaying())
        {
            return;
        }

        if (!playerInRange)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Toggle();
        }
    }

    private void Toggle()
    {
        SetActivated(!GetActivated());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = true;
            Debug.Log("Player Here");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {
            playerInRange = false;
        }
    }
}
