using UnityEngine;

public class DropItem : MonoBehaviour
{
    [Header("Pickup Settings")]
    public int minAmount = 1;
    public int maxAmount = 3;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            BallThrower thrower = other.GetComponent<BallThrower>();
            if (thrower != null)
            {
                int amount = Random.Range(minAmount, maxAmount + 1);
                thrower.CollectBall(amount);
                Debug.Log("🎁 ผู้เล่นเก็บ Drop → + " + amount + " ลูกบอล");

                
                ObjectPool.Instance.ReturnObject(gameObject);
            }
        }
    }
}
