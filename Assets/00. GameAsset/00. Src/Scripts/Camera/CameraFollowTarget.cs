using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform   target;
    [SerializeField]
    private int         followSpeed;
    [SerializeField]
    private Vector3     offset;
    [SerializeField]
    private bool        isFollowing;

    private void Start()
    {
        transform.position = target.position + offset;
    }

    private void FixedUpdate()
    {
        if(!isFollowing) return;

        Vector3 targetPosition = target.position + offset;
        transform.position     = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
    }
    
    public Vector3 GetOffset() { return offset; }

    public void SetOffset(Vector3 offset)
    {
        this.offset = offset;
    }
}
