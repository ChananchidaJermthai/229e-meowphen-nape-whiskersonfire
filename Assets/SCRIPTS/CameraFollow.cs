using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // ตัวผู้เล่น
    public Vector3 offset = new Vector3(0, 0, -10); // กล้องควรอยู่ Z = -10 สำหรับ 2D
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = offset.z; // ล็อค Z ให้กล้องไม่เข้าไปใกล้/ไกล

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

