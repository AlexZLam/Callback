using UnityEngine;

[System.Serializable]
public class SwitchCondition
{
    public SwitchController connectedSwitch;
    public bool requiredState = true;
}

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

    [Header("Switch Conditions")]
    public SwitchCondition[] switchConditions;

    // Preserve existing scene connections as normal (ON) conditions.
    [SerializeField, HideInInspector] public SwitchController[] connectedSwitches;

    private void Awake()
    {
        if (doorCollider == null)
            doorCollider = GetComponent<BoxCollider2D>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
            spriteRenderer.enabled = true;
        }

        // Migrate old connections when no new conditions have been configured.
        if ((switchConditions == null || switchConditions.Length == 0) &&
            connectedSwitches != null && connectedSwitches.Length > 0)
        {
            switchConditions = new SwitchCondition[connectedSwitches.Length];

            for (int i = 0; i < connectedSwitches.Length; i++)
            {
                switchConditions[i] = new SwitchCondition
                {
                    connectedSwitch = connectedSwitches[i],
                    requiredState = true
                };
            }
        }

        setOpen(isOpen);
    }

    private void OnEnable()
    {
        if (switchConditions != null)
        {
            foreach (SwitchCondition condition in switchConditions)
            {
                if (condition != null && condition.connectedSwitch != null)
                    condition.connectedSwitch.RegisterDoor(this);
            }
        }

        UpdateDoorState();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        UpdateDoorState();
    }

    private void OnDisable()
    {
        if (switchConditions == null)
            return;

        foreach (SwitchCondition condition in switchConditions)
        {
            if (condition != null && condition.connectedSwitch != null)
                condition.connectedSwitch.UnregisterDoor(this);
        }
    }

    // Opens or closes the door and updates its sprite and collision.
    public void setOpen(bool open)
    {
        isOpen = open;

        if (doorCollider != null)
            doorCollider.enabled = !isOpen;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
            Sprite stateSprite = isOpen ? openSprite : closedSprite;

            if (stateSprite != null)
                spriteRenderer.sprite = stateSprite;
        }
    }

    // Opens only when every connected switch matches its required state.
    public void UpdateDoorState()
    {
        if (switchConditions == null || switchConditions.Length == 0)
        {
            setOpen(false);
            return;
        }

        foreach (SwitchCondition condition in switchConditions)
        {
            if (condition == null || condition.connectedSwitch == null ||
                condition.connectedSwitch.GetActivated() != condition.requiredState)
            {
                setOpen(false);
                return;
            }
        }

        setOpen(true);
    }

    public bool GetIsOpen()
    {
        return isOpen;
    }
}
