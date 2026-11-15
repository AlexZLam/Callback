using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Switch State")]
    [SerializeField] public bool isActivated = false;

    // Retain the original serialized reference for existing scenes.
    [SerializeField, HideInInspector] public DoorController connectedDoor;

    [Header("Switch Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private readonly HashSet<DoorController> connectedDoors = new HashSet<DoorController>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetActivated(isActivated);
    }

    protected virtual void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool GetActivated()
    {
        return isActivated;
    }

    // Doors register themselves using their configured switch conditions.
    public void RegisterDoor(DoorController door)
    {
        if (door != null)
            connectedDoors.Add(door);
    }

    public void UnregisterDoor(DoorController door)
    {
        connectedDoors.Remove(door);
    }

    public void SetActivated(bool activated)
    {
        isActivated = activated;
        UpdateVisuals();

        foreach (DoorController door in connectedDoors)
        {
            if (door != null)
                door.UpdateDoorState();
        }
    }

    protected virtual void UpdateVisuals()
    {
        if (spriteRenderer == null)
            return;

        spriteRenderer.color = Color.white;
        Sprite stateSprite = isActivated ? activeSprite : inactiveSprite;

        if (stateSprite != null)
            spriteRenderer.sprite = stateSprite;
    }
}
