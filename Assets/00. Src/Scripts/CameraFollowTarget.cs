using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform   target;
    [SerializeField]
    private int         followSpeed;
    [SerializeField]
    private Vector3     offset;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position     = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
}
