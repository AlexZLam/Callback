using Unity.VisualScripting;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] public bool isActivated = false;


    [SerializeField] public DoorController connectedDoor;

    public SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
        if (connectedDoor != null)
        {
            if (isActivated)
            {
                spriteRenderer.color = connectedDoor.activeColor;
            }
            else
            {
                spriteRenderer.color = connectedDoor.inactiveColor;
            }

            if (connectedDoor != null)
            {
                
                connectedDoor.UpdateDoorState();
            }
        }
    }

}
