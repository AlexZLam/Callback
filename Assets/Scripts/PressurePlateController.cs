using System.Collections.Generic;
using UnityEngine;

public class PressurePlateController : SwitchController
{
    private HashSet<Collider2D> pressingObjects;
    public LayerMask activationLayers;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Awake()
    {
        base.Awake();
        pressingObjects = new HashSet<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger) return;

        if ((activationLayers.value & (1 << other.gameObject.layer)) == 0) return;
        
        pressingObjects.Add(other);

        UpdateActivation();

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressingObjects.Remove(other);

        UpdateActivation();
    }

    private void UpdateActivation()
    {
        if (pressingObjects.Count > 0) SetActivated(true);

        else SetActivated(false);
    }
}
