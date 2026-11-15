
using UnityEngine;

public class Switch_Callback : Callback_Entity
{
    [Header("Switch References")]
    public SwitchController switchController;
    public bool saveActivated;

    [Header("Ghost Settings")]
    public GameObject ghostPrefab;

    private GameObject ghostObject;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer ghostRenderer;

    private void Awake()
    {
        switchController = GetComponent<SwitchController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void CaptureState()
    {
        saveActivated = switchController.GetActivated();

        CreateGhost();
    }

    protected override void RestoreState()
    {
        switchController.SetActivated(saveActivated);
    }

    // Creates a ghost showing the saved switch state.
    private void CreateGhost()
    {
        DestroyGhost();

        ghostObject = Instantiate(
            ghostPrefab,
            transform.position,
            transform.rotation
        );

        ghostRenderer = ghostObject.GetComponent<SpriteRenderer>();

        // Copy the switch's current appearance.
        ghostRenderer.sprite = spriteRenderer.sprite;
        ghostRenderer.flipX = spriteRenderer.flipX;
        ghostRenderer.flipY = spriteRenderer.flipY;

        ghostObject.transform.localScale = transform.lossyScale;

        // Use the same purple tint as Physics_Callback.
        ghostRenderer.color = new Color(0.63f, 0.13f, 0.94f, 0.45f);

        // Display the ghost above the actual switch.
        ghostRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
        ghostRenderer.sortingOrder = spriteRenderer.sortingOrder + 1;
    }

    // Removes the existing ghost.
    private void DestroyGhost()
    {
        if (ghostObject != null)
            Destroy(ghostObject);

        ghostObject = null;
    }

    // Clears the saved callback and removes its ghost.
    public override void ClearCallback()
    {
        base.ClearCallback();

        DestroyGhost();
    }

    private void OnDestroy()
    {
        DestroyGhost();
    }
}
