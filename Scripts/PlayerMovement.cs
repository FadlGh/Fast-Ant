using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    public float speed;
    public float jumpingPower;
    private bool isFacingRight = true;

    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    private float jumpBufferTimeCounter;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower;
    private float dashingTime = 0.2f;
    private float dashingCoolDown = 1f;

    private bool isRespawning;

    public Transform shootPoint;
    public GameObject bulletPre;
    public float shootSpeed;
    private float timeBetween;
    private float timeBetweenCounter = 0.5f;
    private bool canshoot;

    public ParticleSystem flipPs;

    [SerializeField] private Animator am;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    void Start()
    {
        StartCoroutine(FadeIn());
        timeBetween = timeBetweenCounter;
    }
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (isDashing || isRespawning)
        {
            return;
        }
        #region Coyote and Buffer
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimeCounter = jumpBufferTime; 
        }
        else
        {
            jumpBufferTimeCounter -= Time.deltaTime;
        }
        #endregion

        #region Jump and dash
        if (jumpBufferTimeCounter > 0f && coyoteTimeCounter > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            jumpBufferTimeCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f; 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) & canDash)
        {
            StartCoroutine(Dash());
        }
        #endregion

        if (timeBetween < 0)
        {
            canshoot = true;
        }
        else
        {
            canshoot = false;
            timeBetween -= Time.deltaTime;
        }
        print(canshoot);
        if (Input.GetButtonDown("shoot") & canshoot)
        {
            Shoot();
            timeBetween = timeBetweenCounter;
        }
        Flip();
    }

    private void FixedUpdate()
    {
        if (isDashing || isRespawning)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        am.SetFloat("Speed", Mathf.Abs(horizontal));
        am.SetBool("isRespawning", false);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.02f, groundLayer);
    }

    void Shoot()
    {
        int direction()
        {
            if(transform.localScale.x < 0f)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }
        print("s");
        GameObject bullet = Instantiate(bulletPre, shootPoint.position, Quaternion.Euler(0,0,-90)) ;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootSpeed * direction() * Time.fixedDeltaTime, 0);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            if (IsGrounded()) Instantiate(flipPs, groundCheck.position, Quaternion.identity);
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCoolDown);
        canDash = true;
    }

    private IEnumerator FadeIn()
    {
        isRespawning = true;
        yield return new WaitForSeconds(2f);
        isRespawning = false;
    }
}