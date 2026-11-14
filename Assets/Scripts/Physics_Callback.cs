using UnityEngine;

public class Physics_Callback : Callback_Entity
{
    [SerializeField]
    private Rigidbody2D rb;
    private Vector2 SavedPosition;
    private float SavedRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void CaptureState()
    {
        SavedPosition = rb.position;
        SavedRotation = rb.rotation;
    }

    protected override void RestoreState()
    {
        rb.position = SavedPosition;
        rb.rotation = SavedRotation;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
