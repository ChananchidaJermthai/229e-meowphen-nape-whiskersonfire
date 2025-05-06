using UnityEngine;
using System.Collections;
using UnityEngine.Analytics;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [SerializeField] private TextMeshProUGUI heartcount;
    [SerializeField]private int heart;

    private Rigidbody2D rb;
    private Animator anim;

    private bool isGrounded;
    public bool GameOver;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector3 originalScale;
    private Coroutine uprightRoutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;

        heart = 3;
        GameOver = false;
        isGrounded = false;
    }

    void Update()
    {
        
        float moveX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!isGrounded && uprightRoutine == null)
        {
            uprightRoutine = StartCoroutine(RotateToUprightInAir());
        }

        if (isGrounded && uprightRoutine != null)
        {
            StopCoroutine(uprightRoutine);
            uprightRoutine = null;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            rb.angularVelocity = 0f;
        }

        anim.SetFloat("Speed", Mathf.Abs(moveX));
        anim.SetBool("isGrounded", isGrounded);

        if (moveX > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);

        Isdie();
        UpdateText();
    }
    public void UpdateText()
    {

        heartcount.text = heart.ToString();
        
    }

    void FreezdZ()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            this.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            Debug.Log("Reset Rotation");
        }
    }

    void Isdie()
    {
        if (heart <= 0)
        {
            GameOver = true;
        }
    }

    void TakeDamage()
    {
        heart -= 1;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sprite"))
        {
            Debug.Log(" - Hp !");
            TakeDamage();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
        }
    }

    private IEnumerator RotateBackToUpright()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    private IEnumerator RotateToUprightInAir()
    {
        while (!isGrounded)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
            yield return null;
        }
    }
}

