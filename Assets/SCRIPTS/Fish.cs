using UnityEngine;

public class Fish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

             ObjectPool.Instance.ReturnObject(gameObject);
            
        }
    }
}
