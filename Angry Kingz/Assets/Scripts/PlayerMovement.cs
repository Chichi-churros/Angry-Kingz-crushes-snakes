using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    // The Force added upon release
    public float force = 600;
    public GameObject slingshot;
    public Vector2 startPos;

    private Vector3 velocity = Vector3.zero;
    private float horizontalMovement;


    private void Start()
    {
        startPos = slingshot.transform.position;
    }


    // Update is called once per frame
    void Update(){

        horizontalMovement = Input.GetAxis ("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown ("Jump") && isGrounded) {
            isJumping = true;
        }

        Flip (rb.velocity.x);

        float characterVelocity = Mathf.Abs (rb.velocity.x);
        animator.SetFloat ("Speed", characterVelocity);

    }

    void FixedUpdate () {

        MovePlayer (horizontalMovement);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);

      
    }

    void MovePlayer (float _horizontalMovement) {
        Vector3 targetVelocity = new Vector2 (_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp (rb.velocity, targetVelocity, ref velocity, .05f);

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
        Vector2 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // convertit la position de la souris en world position

        float radius = 1.8f;
        Vector2 dir = p - startPos;

        if (dir.sqrMagnitude > radius)
            dir = dir.normalized * radius;

        transform.position = startPos + dir;
    }

    private void OnMouseUp()
    {
        rb.constraints = RigidbodyConstraints2D.None;   // Free from constraints
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; // set rotation constraints

        rb.isKinematic = false;

        Vector2 dir = startPos - (Vector2)transform.position;
        rb.AddForce(dir * force);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}