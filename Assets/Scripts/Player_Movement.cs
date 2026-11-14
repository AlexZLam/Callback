using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public BoxCollider2D groundCheck;
    public LayerMask groundLayer;

    public Rigidbody2D rb;
    public bool isGrounded;
    private float moveInput;
    private bool jumpRequested;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Diagnostic 1: Check if references or layer masks are empty
        if (groundCheck == null)
            Debug.LogError("ERROR: Ground Check BoxCollider2D is NOT assigned in the Inspector!");
        if (groundLayer.value == 0)
            Debug.LogError("ERROR: Ground Layer is set to 'Nothing' in the Inspector!");
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        else if (moveInput > 0)
            GetComponent<SpriteRenderer>().flipX = false;

        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jump pressed! isGrounded = " + isGrounded);
            if (isGrounded)
            {
                jumpRequested = true;
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Diagnostic 2: Check what the box is actually touching (ignoring layer filters)
        //ContactFilter2D noFilter = ContactFilter2D.noFilter;
        //Collider2D[] hits = new Collider2D[5];
        //int hitCount = groundCheck.Overlap(noFilter, hits);

        isGrounded = groundCheck.IsTouchingLayers(groundLayer);

      /*  if (!isGrounded && hitCount > 0)
        {
            Debug.LogWarning($"groundCheck IS touching '{hits[0].name}' (Layer: {LayerMask.LayerToName(hits[0].gameObject.layer)}), which does NOT match your Ground Layer!");
        }*/

        if (jumpRequested)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }
    }
}
