using UnityEngine;
using System.Collections;

public class AutoReturnToPool : MonoBehaviour
{
    public float lifetime = 1f; // เวลาที่ effect อยู่ก่อนหาย

    private void OnEnable()
    {
        StartCoroutine(ReturnAfterTime());
    }

    private IEnumerator ReturnAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        ObjectPool.Instance.ReturnObject(gameObject);
    }
}
