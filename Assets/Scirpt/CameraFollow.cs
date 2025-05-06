using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;                 // ��Ǽ�����
    public Vector3 offset = new Vector3(0, 0, -10); // ���ͧ������� Z = -10 ����Ѻ 2D
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = offset.z; // ��ͤ Z �����ͧ����������/��

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

