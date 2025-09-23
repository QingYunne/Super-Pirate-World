using System.Collections;
//using UnityEditor.Rendering;
//using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

//10.3125f;
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 3.125f;
    [SerializeField] private float gravity = 20.3125f;
    [SerializeField] private float jumpForce = 14.0625f;
    [SerializeField] private float wallJumpForceX = 7f;
    [SerializeField] private float wallJumpForceY = 14f;
    private bool isWallJumping = false;
    private float wallJumpTimer = 0f;
    [SerializeField] private float wallJumpDuration = 0.2f; 
    private Transform hitbox;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    Vector2 direction;
    private bool isJumping = false;

    [Header("Ground Check Settings")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform leftCheck;
    [SerializeField] private Transform rightCheck;
    [SerializeField] private Vector2 groundBoxSize = new Vector2(1.2f, 0.2f);
    [SerializeField] private Vector2 wallBoxSize = new Vector2(0.05f, 0.7f);
    [SerializeField] private LayerMask platformLayer;

    [Header("Attack Settings")]
    [SerializeField] private Transform attackOverlap;
    [SerializeField] private float attackRange = 0.2f;
    [SerializeField] private LayerMask damageLayer;
    [SerializeField] private float attackCooldown = 0.5f;
    private bool isAttacking = false;

    //private AudioManager audioManager;


    private PlayerManager playerManager;
    private LayerMask groundLayer;
    private Rigidbody2D rb;
    [SerializeField] private bool onGround, onLeftWall, onRightWall;
    private bool wallJumpBlocked = false;
    private bool wallSlideBlocked = false;
    private bool fallRequest = false;
    private bool isFallingThrough = false;

    private BoxCollider2D box;
    private Vector2 velocity;

    private Vector2 currentPos;
    private Vector2 previousPos;

    private Animator animator;


    void Awake()
    {
        hitbox = transform.Find("Hitbox");
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = hitbox.GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        currentPos = transform.position;
        previousPos = currentPos;
        groundLayer = LayerMask.GetMask("Ground", "Platform");
        animator = GetComponent<Animator>();
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void HandleInput()
    {
        Bounds b = box.bounds;
        Vector3 pos = transform.position;
        if (wallJumpBlocked)
            return;
        float moveX = Input.GetAxisRaw("Horizontal");
        direction.x = moveX;
        if (Input.GetAxis("Vertical") < 0 && !fallRequest)
        {
            fallRequest = true;
            StartCoroutine(FallThroughPlatform());
        }
        if (moveX > 0)
        {
            if (!facingRight)
            {
                pos.x += b.size.x * Time.deltaTime;
                transform.position = pos;
            }
            facingRight = true;
        }
            
        else if (moveX < 0)
        {
            if (facingRight)
            {
                pos.x -= b.size.x * Time.deltaTime;
                transform.position = pos;
            }
            facingRight = false;
        }
            
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        //if (Input.GetAxis("Vertical") < 0 && !fallRequest)
        //{
        //    fallRequest = true;
        //    StartCoroutine(FallThroughPlatform());
        //}

        //if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))  && !fallRequest)
        //{
        //    fallRequest = true;
        //    StartCoroutine(FallThroughPlatform());
        //}

        //if (Input.GetAxis("Vertical") < 0 && rb.linearVelocity.y <= 0)
        //{
        //    rb.linearVelocity = new Vector2(rb.linearVelocity.x, -gravity * 2f); // t?ng t?c r?i xu?ng
        //}

        if (Input.GetKeyDown(KeyCode.J))
        {
            Attack();
        }

    }

    void HandleMovement(float dt)
    {
        // 1. Horizontal input (ignore khi wall jumping)
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }

        // 2. Vertical – 
        if (!onGround)
        {
            rb.linearVelocity += Vector2.down * gravity * dt;
        }


        if (!onGround && (onLeftWall || onRightWall) && !wallSlideBlocked)
        {
            if (rb.linearVelocity.y < -gravity / 10f)
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, -gravity / 10f);
        }

        // 3. Jump logic
        if (isJumping)
        {
            
            if (onGround)
            {
                // Jump from ground
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                //audioManager.PlaySFX(audioManager.jumpClip);
                EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Jump, 1f));
                //StartCoroutine(WallSlideBlock());
            }
            else if ((onLeftWall || onRightWall) && !wallSlideBlocked)
            {
                //audioManager.PlaySFX(audioManager.jumpClip);
                EventManager.Trigger("PlaySFX", new SoundEventData(SoundType.SFX_Jump, 1f));
                // Wall jump - override horizontal
                float horizontalDir = onLeftWall ? 1f : -1f;
                rb.linearVelocity = new Vector2(horizontalDir * wallJumpForceX, wallJumpForceY);
                facingRight = horizontalDir > 0f;

                // Lock input trong khoang wall jump
                isWallJumping = true;
                wallJumpTimer = wallJumpDuration;

                StartCoroutine(WallSlideBlock());
                StartCoroutine(WallJumpCooldown());
            }

            isJumping = false;
        }

        // Timer wall jump - unlock input
        if (isWallJumping)
        {
            wallJumpTimer -= dt;
            if (wallJumpTimer <= 0f)
                isWallJumping = false;
        }

        //// 1. Horizontal input (ignore khi wall jumping)
        //if (!isWallJumping)
        //{
        //    rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        //}

        //// 2. Vertical - Wall Slide / Gravity
        //if (!onGround && (onLeftWall || onRightWall) && !wallSlideBlocked)
        //{
        //    // Wall slide 
        //    if (rb.linearVelocity.y < 0) 
        //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -gravity / 10f));
        //}
        //else if (!onGround)
        //{
        //    rb.linearVelocity += Vector2.down * gravity * dt;
        //}

        //// 3. Jump logic
        //if (isJumping)
        //{
        //    if (onGround)
        //    {
        //        // Jump from ground
        //        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        //        StartCoroutine(WallSlideBlock());
        //    }
        //    else if ((onLeftWall || onRightWall) && !wallSlideBlocked)
        //    {
        //        // Wall jump - override horizontal
        //        float horizontalDir = onLeftWall ? 1f : -1f;
        //        rb.linearVelocity = new Vector2(horizontalDir * wallJumpForceX, wallJumpForceY);
        //        facingRight = horizontalDir > 0f;

        //        // Lock input trong khoang wall jump
        //        isWallJumping = true;
        //        wallJumpTimer = wallJumpDuration;

        //        StartCoroutine(WallSlideBlock());
        //        StartCoroutine(WallJumpCooldown());
        //    }

        //    isJumping = false;
        //}

        //// Timer wall jump - unlock input
        //if (isWallJumping)
        //{
        //    wallJumpTimer -= dt;
        //    if (wallJumpTimer <= 0f)
        //        isWallJumping = false;
        //}
    }


    void Flip()
    {
        //Bounds b = box.bounds;
        ////Vector3 hbScale = hitbox.localScale;
        ////hbScale.x = facingRight ? 1f : -1f;
        ////hitbox.localScale = hbScale;
        ////spriteRenderer.flipX = !facingRight;
        //// Neu dang o tren tuong thi khong flip

        ////if ((facingRight && onRightWall) || (!facingRight && onLeftWall))
        ////    return;
        ////if (facingRight)
        ////{
        ////   pos
        ////    transform.localScale = new Vector3(1f, 1f, 1f);
        ////}
        ////else         {
        ////    transform.localScale = new Vector3(-1f, 1f, 1f);
        ////}
        if (facingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }

    void UpdateCheckTransform()
    {
        Bounds b = box.bounds;

        groundCheck.position = new Vector2(b.center.x, b.min.y);
        //groundCheck.localScale = new Vector3(b.size.x, 0.05f, 1f);

        leftCheck.position = new Vector2(b.min.x, b.center.y);
        //leftCheck.localScale = new Vector3(0.02f, b.size.y / 2f, 1f);

        rightCheck.position = new Vector2(b.max.x, b.center.y);
        //rightCheck.localScale = new Vector3(0.02f, b.size.y / 2f, 1f);
    }

    void CheckSurroundings()
    {
        onGround = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0, groundLayer);
        onLeftWall = Physics2D.OverlapBox(leftCheck.position, wallBoxSize, 0, groundLayer);
        onRightWall = Physics2D.OverlapBox(rightCheck.position, wallBoxSize, 0, groundLayer);
    }

     //Update is called once per frame
    void Update()
    {
        HandleInput();
        Flip();
        UpdateCheckTransform();
        
        CheckSurroundings();
        HandleAnimation();
        HandleFlicker();
        CheckAttackHit();

        //DebugCollison();    
    }

    void FixedUpdate()
    {
        HandleMovement(Time.fixedDeltaTime);
    }

    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
        }
        if (leftCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(leftCheck.position, wallBoxSize);
        }
        if (rightCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(rightCheck.position, wallBoxSize);
        }

        if (attackOverlap != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackOverlap.position, attackRange);
        }

            
    }

    private IEnumerator WallJumpCooldown()
    {
        wallJumpBlocked = true;
        yield return new WaitForSeconds(0.4f);
        wallJumpBlocked = false;
    }

    private IEnumerator WallSlideBlock()
    {
        wallSlideBlocked = true;
        yield return new WaitForSeconds(0.2f);
        wallSlideBlocked = false;
    }

    //public void HandleCollision(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        collisionWithGround = true;
    //        Debug.Log("Colliding with ground");
    //    }
    //}

    //public void HandleCollisionExit(Collider2D collision)
    //{
    //   collisionWithGround = false;
    //}

    //public void DebugCollison()
    //{
    //    if (onGround)
    //        Debug.Log("On Ground");
    //}

    private IEnumerator FallThroughPlatform()
    {

        fallRequest = false;
        isFallingThrough = true;

        Collider2D[] platforms = Physics2D.OverlapBoxAll(groundCheck.position, groundBoxSize, 0, platformLayer);

        foreach (Collider2D platform in platforms)
        {
            Physics2D.IgnoreCollision(box, platform, true);
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -gravity * 2f);
        }

        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        //rb.linearVelocity = new Vector2(rb.linearVelocity.x, -gravity * 2f);

        yield return new WaitForSeconds(0.3f);

        foreach (Collider2D platform in platforms)
        {
            Physics2D.IgnoreCollision(box, platform, false);
        }
        isFallingThrough = false;

    }

    void HandleAnimation()
    {
        animator.SetFloat("isRunning", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("YVelocity", rb.linearVelocity.y);
        animator.SetBool("onGround", onGround);
        animator.SetBool("onWall", (onLeftWall || onRightWall) && !onGround && !isFallingThrough);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(null);
        }
    }

    private void HandleFlicker()
    {
        if (PlayerManager.Instance != null && PlayerManager.Instance.IsInvincible)
        {
            float sine = Mathf.Sin(Time.time * 40); 
            spriteRenderer.enabled = sine > 0;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    public void Attack()
    {
        if (!isAttacking)
            StartCoroutine(DoAttack());
    }

    private IEnumerator DoAttack()
    {
        isAttacking = true;

        if (onGround)
            animator.SetTrigger("Attack");
        else
            animator.SetBool("AirAttack", true);


        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        animator.SetBool("AirAttack", false);
    }

    public void CheckAttackHit()
    {
        if (!isAttacking)
            return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(attackOverlap.position, attackRange, damageLayer);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Pearl") || hit.CompareTag("Tooth"))
            {
                var enemy = hit.GetComponent<IEnemy>();
                if (enemy != null)
                    enemy.Reverse();
            }
        }
    }
}
