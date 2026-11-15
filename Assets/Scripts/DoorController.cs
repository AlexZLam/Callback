
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Door State")]
    private bool isOpen = false;

    [Header("Door Components")]
    public BoxCollider2D doorCollider;
    public SpriteRenderer spriteRenderer;

    [Header("Door Sprites")]
    public Sprite openSprite;
    public Sprite closedSprite;

    [Header("Connected Switches")]
    public SwitchController[] connectedSwitches;

    // Initialize the door components.
    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Remove any tint from the old implementation.
        spriteRenderer.color = Color.white;

        setOpen(isOpen);
    }

    private void Start()
    {
        UpdateDoorState();
    }

    // Opens or closes the door.
    public void setOpen(bool open)
    {
        isOpen = open;

        // Disable collision when the door is open.
        doorCollider.enabled = !isOpen;

        // Update the door sprite based on its state.
        if (isOpen)
        {
            spriteRenderer.sprite = openSprite;
        }
        else
        {
            spriteRenderer.sprite = closedSprite;
        }
    }

    // Checks whether all connected switches are activated.
    public void UpdateDoorState()
    {
        if (connectedSwitches == null ||
            connectedSwitches.Length == 0)
        {
            setOpen(false);
            return;
        }

        for (int i = 0; i < connectedSwitches.Length; i++)
        {
            if (connectedSwitches[i] == null ||
                !connectedSwitches[i].GetActivated())
            {
                setOpen(false);
                return;
            }
        }

        setOpen(true);
    }

    // Returns the current state of the door.
    public bool GetIsOpen()
    {
        return isOpen;
    }
}
