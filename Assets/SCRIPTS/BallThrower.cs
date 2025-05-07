using UnityEngine;
using System.Collections;
using TMPro;

public class BallThrower : MonoBehaviour
{
    [Header("Ball Settings")]
    public GameObject ballPrefab;
    public Transform firePoint;
    public float travelTime = 1f; // เวลาที่ลูกบอลจะใช้ไปถึงเป้าหมาย
    public float cooldownTime = 1f;
    public TextMeshProUGUI BallCountText;
    public int ballCount = 3;

    private bool canShoot = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && ballCount > 0)
        {
            StartCoroutine(ShootBall());
        }

        UpdateText();
    }

    public void UpdateText()
    {
        BallCountText.text = ballCount.ToString();
    }

    IEnumerator ShootBall()
    {
        canShoot = false;

        // หาตำแหน่งเมาส์ในโลก
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetPosition = new Vector2(mousePos.x, mousePos.y);

        // ดึงลูกบอลจาก ObjectPool
        GameObject ball = ObjectPool.Instance.GetObject(ballPrefab);
        ball.transform.position = firePoint.position;
        ball.transform.rotation = Quaternion.identity;

        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        rb.linearVelocity = CalculateProjectileVelocity(firePoint.position, targetPosition, travelTime);

        ballCount--;

        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }


    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;
        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;
        return new Vector2(velocityX, velocityY);
    }

    public void CollectBall(int amount)
    {
        ballCount += amount;
        Debug.Log("🟢 เก็บบอล: ตอนนี้มี " + ballCount + " ลูก");
    }
}
