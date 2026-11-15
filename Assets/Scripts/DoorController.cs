using UnityEngine;

public class DoorController : MonoBehaviour
{
    private bool isOpen = false;
    public BoxCollider2D doorCollider;
    public SpriteRenderer spriteRenderer;
    public SwitchController[] connectedSwitches;
    public Color activeColor;
    public Color inactiveColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        setOpen(isOpen);
    }

    public void setOpen(bool open)
    {
        isOpen = open;
        doorCollider.enabled = !isOpen;
        spriteRenderer.enabled = !isOpen;
        spriteRenderer.color = inactiveColor;
    }

    public void UpdateDoorState()
    {
        if (connectedSwitches == null)
        {
            setOpen(false);
            return;
        }

        for(int i = 0; i < connectedSwitches.Length; i++)
        {
            if (!connectedSwitches[i].isActivated)
            {
                setOpen(false);
                return;
            }
        }

        setOpen(true);
    }

    private Color getActiveColor()
    {
        return activeColor;
    }

    private Color getInactiveColor()
    {
        return inactiveColor;
    }
    

   
}
