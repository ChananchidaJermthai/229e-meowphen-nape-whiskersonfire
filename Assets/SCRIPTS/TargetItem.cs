using UnityEngine;

public class TargetItem : MonoBehaviour
{
    [Header("Effect")]
    public GameObject hitEffectPrefab;

    [Header("Drop Settings")]
    public GameObject[] dropPrefabs; // รายการ item ที่จะสุ่มดรอป
    [Range(0, 100)] public int dropChancePercent = 100; // โอกาสดรอปจริง (0–100)

    public void OnHit()
    {
        // 🎆 สร้าง Effect (จาก Pool)
        if (hitEffectPrefab != null)
        {
            GameObject effect = ObjectPool.Instance.GetObject(hitEffectPrefab);
            effect.transform.position = transform.position;
        }

        // 🎁 สุ่มว่าจะดรอปไหม
        int roll = Random.Range(0, 100);
        if (roll < dropChancePercent && dropPrefabs.Length > 0)
        {
            // สุ่ม 1 อย่างจาก list
            int index = Random.Range(0, dropPrefabs.Length);
            GameObject drop = ObjectPool.Instance.GetObject(dropPrefabs[index]);
            drop.transform.position = transform.position;
        }

        // ❌ หายไป (หรือคืนเข้า Pool ถ้าใช้ Pool สำหรับ TargetItem)
        gameObject.SetActive(false);
    }
}
