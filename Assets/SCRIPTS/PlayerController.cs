using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal");

        bool isRunning = move != 0;
        animator.SetBool("isRunning", isRunning);

        Debug.Log("isRunning: " + isRunning);  // << ´Ùã¹ Console

        transform.position += new Vector3(move, 0, 0) * Time.deltaTime * 5f;
    }

}
