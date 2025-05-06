using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        // เคลื่อนที่ซ้าย-ขวา
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);

        // กระโดดเมื่อกดปุ่ม Jump และอยู่บนพื้น
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // เช็กว่าติดพื้นหรือไม่
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // ส่งค่าพารามิเตอร์ไปยัง Animator
        anim.SetFloat("Speed", Mathf.Abs(moveX));
        anim.SetBool("isGrounded", isGrounded);

        // พลิกตัวละครโดยรักษาสเกลเดิม
        if (moveX > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        else if (moveX < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
    }
}
