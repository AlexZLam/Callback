
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField] public bool isActivated = false;

    [Header("Connected Door")]
    [SerializeField] public DoorController connectedDoor;

    [Header("Switch Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    // Start is called once before the first execution of Update
    // after the MonoBehaviour is created
    void Start()
    {
        SetActivated(isActivated);
    }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool GetActivated()
    {
        return isActivated;
    }

    public void SetActivated(bool activated)
    {
        isActivated = activated;

        UpdateVisuals();

        if (connectedDoor != null)
        {
            connectedDoor.UpdateDoorState();
        }
    }

    protected virtual void UpdateVisuals()
    {
        if (spriteRenderer == null)
            return;

        // Remove any tint left over from the old implementation.
        spriteRenderer.color = Color.white;

        if (isActivated)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = inactiveSprite;
        }
    }
}
