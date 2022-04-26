using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    public bool isGrounded;
    public bool isLaunching;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // Catapulte...
    public float force = 600;
    public GameObject slingshot;
    public Vector2 slingshotPos;

    public Vector2 startPos;
    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;

    private void Start()
    {
        slingshotPos = slingshot.transform.position;
        startPos = transform.position;
        isLaunching = false;
    }


    // =========== Update =========== //
    void Update(){

        horizontalMovement = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;

        // Test launch
        if(isLaunching && isGrounded)
        {
            isLaunching = false;
        }
        // Test saut
        if (Input.GetButtonDown ("Jump") && isGrounded && !isLaunching) {
            isJumping = true;
        }

        Flip (rb.velocity.x);

        float characterVelocity = Mathf.Abs (rb.velocity.x);
        animator.SetFloat ("Speed", characterVelocity);

        MovePlayer(horizontalMovement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

    }


    void MovePlayer (float _horizontalMovement) 
    {
        // Autorise le déplacement horizontal si pas en "mode launching"
        if (!isLaunching)
        {
            Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);
        }
        

        if (isJumping) {
            rb.AddForce (new Vector2 (0f, jumpForce));
            isJumping = false;
        }

    }


    void Flip (float _velocity) {
        if (_velocity > 0.1f) {
            spriteRenderer.flipX = false;
        } else if (_velocity < -0.1f) {
            spriteRenderer.flipX = true;
        }
    }


    private void OnMouseDrag()
    {
        if (!isLaunching)
        {
            isLaunching = true;
        }

        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // convertit la position de la souris en world position

        float radius = 1.5f;
        Vector2 dir = p - slingshotPos;

        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;

        transform.position = slingshotPos + dir;
    }


    private void OnMouseUp()
    {

        rb.constraints = RigidbodyConstraints2D.None;   // Free from constraints
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // set rotation constraints

        rb.isKinematic = false;

        Vector2 dir = slingshotPos - (Vector2)transform.position;
        rb.AddForce(dir * force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Death"))
        {
            transform.position = startPos;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}