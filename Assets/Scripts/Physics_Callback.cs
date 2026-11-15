using UnityEngine;

public class Physics_Callback : Callback_Entity
{
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 SavedPosition;
    private float SavedRotation;
    public GameObject ghostPrefab;
    private GameObject ghostObject;
    private SpriteRenderer spriteRenderer;
    private SpriteRenderer ghostRenderer;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void CaptureState()
    {
        SavedPosition = rb.position;
        SavedRotation = rb.rotation;
        CreateGhost();
    }

    protected override void RestoreState()
    {
        rb.position = SavedPosition;
        rb.rotation = SavedRotation;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
    
    private void CreateGhost()
    {
        DestroyGhost();

        ghostObject = Instantiate(ghostPrefab, SavedPosition, Quaternion.Euler(0f, 0f, SavedRotation));
        ghostRenderer = ghostObject.GetComponent<SpriteRenderer>();
        ghostRenderer.flipX = spriteRenderer.flipX;
        ghostRenderer.flipY = spriteRenderer.flipY;
        ghostRenderer.sprite = spriteRenderer.sprite;
        ghostObject.transform.localScale = gameObject.transform.lossyScale;
        ghostRenderer.color = new Color(0.63f, 0.13f, 0.94f, 0.45f);
        
    }

    private void DestroyGhost()
    {
        if(ghostObject != null) Destroy(ghostObject);
        ghostObject = null;
    }

    public override void ClearCallback()
    {
        base.ClearCallback();
        DestroyGhost();
    }
}
