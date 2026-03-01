//using UnityEngine;

//public class Player : MonoBehaviour
//{
//    [SerializeField]
//    public float moveForce = 10f;

//    [SerializeField]
//    public float jumpForce = 11f;

//    private float movementX;

//    private Rigidbody2D mybBody;

//    private SpriteRenderer sr;

//    private Animator anim;
//    private string WALK_ANIMATION = "Walk";
//    private bool isGrounded = true;
//    private string GROUNG_TAG = "Ground";

//    private void Awake()
//    {
//        mybBody = GetComponent<Rigidbody2D>();
//        anim = GetComponent<Animator>();
//        sr = GetComponent<SpriteRenderer>();
//    }

//    // Start is called once before the first execution of Update after the MonoBehaviour is created
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        PlayerMoveKeyBoard();
//        AnimatePlayer();
//    }

//    private void FixedUpdate()
//    {
//        PlayerJump();
//    }

//    void PlayerMoveKeyBoard()
//    {
//        movementX = Input.GetAxisRaw("Horizontal");
//        transform.position += new Vector3(movementX, 0f, 0f) * moveForce * Time.deltaTime;
//    }

//    void AnimatePlayer()
//    {
//        // we are going to the right side
//        if (movementX > 0)
//        {
//            anim.SetBool(WALK_ANIMATION, true);
//            sr.flipX = false;
//        }
//        // we are going to the left side
//        else if (movementX < 0)
//        {
//            anim.SetBool(WALK_ANIMATION, true);
//            sr.flipX = true;
//        }
//        else
//        {
//            anim.SetBool(WALK_ANIMATION, false);
//        }
//    }

//    void PlayerJump()
//    {
//        if (Input.GetButtonDown("Jump") && isGrounded)
//        {
//            isGrounded = false;
//            mybBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
//        }
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if (collision.gameObject.CompareTag(GROUNG_TAG)) 
//        {
//            isGrounded=true;
//        }
//    }
//}
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 12f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    private float movementX;
    private bool jumpRequest;
    private bool isGrounded;

    private const string WALK_ANIMATION = "Walk";

    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        rb.freezeRotation = true; // Important for platformers
    }

    private void Update()
    {
        // Read input here
        movementX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    // ---------------- MOVEMENT ----------------

    private void HandleMovement()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = movementX * moveSpeed;
        rb.linearVelocity = velocity;
    }

    // ---------------- JUMP ----------------

    private void HandleJump()
    {
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
            isGrounded = false;
        }
    }

    // ---------------- GROUND DETECTION ----------------

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = false;
        }
    }

    // ---------------- ANIMATION ----------------

    private void AnimatePlayer()
    {
        if (movementX > 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = false;
        }
        else if (movementX < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }
}