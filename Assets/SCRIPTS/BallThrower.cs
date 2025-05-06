using UnityEngine;
using System.Collections;

public class BallThrower : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform firePoint;         // จุดที่ลูกบอลถูกยิงออก
    public float shootForce = 10f;      // ความแรงของการโยน
    public float cooldownTime = 1f;     // ระยะเวลาระหว่างโยนแต่ละลูก
    public int ballCount = 3;           // จำนวนบอลที่มี

    private bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && ballCount > 0)
        {
            StartCoroutine(ShootBall());
        }
    }

    IEnumerator ShootBall()
    {
        canShoot = false;

        // หาตำแหน่งเมาส์ในโลกจริง
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        // คำนวณทิศทางจากผู้เล่นไปยังเมาส์
        Vector2 direction = (mousePos - firePoint.position).normalized;

        // ดึงลูกบอลจาก Pool
        GameObject ball = ObjectPool.Instance.GetObject(ballPrefab);
        ball.transform.position = firePoint.position;
        ball.transform.rotation = Quaternion.identity;

        // ปาบอลด้วยแรง
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero; // Reset ก่อนโยน
        rb.angularVelocity = 0f;
        rb.AddForce(direction * shootForce, ForceMode2D.Impulse);

        // หักจำนวนลูกบอลที่เหลือ
        ballCount--;

        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    // เรียกเมื่อผู้เล่นเก็บบอลจากแมพ
    public void CollectBall(int amount)
    {
        ballCount += amount;
        Debug.Log("🟢 เก็บบอล: ตอนนี้มี " + ballCount + " ลูก");
    }
}


