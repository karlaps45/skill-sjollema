using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int coins;
    public float movespeed = 5f;
    public float JumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public int health = 100;
    public Image healthbar;
    public AudioClip jumpSound;
    public AudioClip damageSound;
    public AudioClip coinSound;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public int extrajumpsvalue = 1;
    private int extrajumps;
    private AudioSource audioSource;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        extrajumps = extrajumpsvalue;
    }

    private void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * movespeed, rb.linearVelocity.y);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
                Playsfx(jumpSound);
            }
            else if (extrajumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);
                extrajumps--;
                Playsfx(jumpSound);
            }
        }

        SetAnimation(moveInput);
        healthbar.fillAmount = health / 100f;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (isGrounded)
        {
            extrajumps = extrajumpsvalue;
        }
    }

    private void SetAnimation(float moveinput)
    {
        if (isGrounded)
        {
            if (moveinput == 0)
            {
                animator.Play("player_1");
            }
            else
            {
                animator.Play("player_run");
            }
        }
        else
        {
            // ✅ FIXED: correct velocity check
            if (rb.linearVelocity.y > 0)
            {
                animator.Play("player_jump1");
            }
            else
            {
                animator.Play("player_fall");
            }
        }

        // ✅ Optional: flip sprite based on movement
        if (moveinput > 0)
            spriteRenderer.flipX = false;
        else if (moveinput < 0)
            spriteRenderer.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("damage"))
        {
            Playsfx(damageSound);
            health -= 25;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpForce);

            StartCoroutine(BlinkRed());

            if (health <= 0)
            {
                Die();
            }
        }
    }

    private IEnumerator BlinkRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void Playsfx(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}