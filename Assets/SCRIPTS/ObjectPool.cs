using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [System.Serializable]
    public class PoolItem
    {
        public GameObject prefab;
        public int amount = 5;
    }

    [Header("All Poolable Prefabs")]
    public PoolItem[] poolItems;

    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // สร้าง Queue สำหรับแต่ละ prefab
        foreach (PoolItem item in poolItems)
        {
            Queue<GameObject> newQueue = new Queue<GameObject>();

            for (int i = 0; i < item.amount; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                newQueue.Enqueue(obj);
            }

            poolDictionary.Add(item.prefab, newQueue);
        }
    }

    // เรียก prefab ตามประเภท
    public GameObject GetObject(GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(prefab))
        {
            Debug.LogWarning("❌ ไม่มี Pool สำหรับ prefab นี้: " + prefab.name);
            return null;
        }

        Queue<GameObject> queue = poolDictionary[prefab];

        if (queue.Count > 0)
        {
            GameObject obj = queue.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(prefab); // กรณีเกินจำนวน
            return obj;
        }
    }

    // คืนวัตถุเข้าคลัง
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);

        foreach (var kvp in poolDictionary)
        {
            if (kvp.Key.name == obj.name.Replace("(Clone)", "").Trim())
            {
                kvp.Value.Enqueue(obj);
                return;
            }
        }

        // ถ้าไม่เจอ pool ที่ตรง → ทำลายทิ้ง
        Debug.LogWarning("⚠️ Object ไม่มี Pool ตรงกัน → ทำลาย: " + obj.name);
        Destroy(obj);
    }
}
