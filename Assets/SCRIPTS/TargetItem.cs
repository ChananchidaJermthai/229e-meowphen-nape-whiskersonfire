using UnityEngine;

public class TargetItem : MonoBehaviour
{
    [Header("Effect")]
    public GameObject hitEffectPrefab;

    [Header("Drop Settings")]
    public GameObject[] dropPrefabs; 
    [Range(0, 100)] public int dropChancePercent = 100; 
    public void OnHit()
    {
        
        if (hitEffectPrefab != null)
        {
            GameObject effect = ObjectPool.Instance.GetObject(hitEffectPrefab);
            effect.transform.position = transform.position;
        }

        
        int roll = Random.Range(0, 100);
        if (roll < dropChancePercent && dropPrefabs.Length > 0)
        {
            
            int index = Random.Range(0, dropPrefabs.Length);
            GameObject drop = ObjectPool.Instance.GetObject(dropPrefabs[index]);
            drop.transform.position = transform.position;
        }

        
        gameObject.SetActive(false);
    }
}
