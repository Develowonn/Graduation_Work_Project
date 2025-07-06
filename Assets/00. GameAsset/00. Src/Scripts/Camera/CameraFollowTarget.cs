using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField]
    private Transform           target;
    [SerializeField]
    private int                 followSpeed;
    [SerializeField]
    private bool                isFollowing;

    [Header("Offset")]
    [SerializeField]
    private CameraOffsetData[]  cameraOffsetData;
    private int                 offsetIndex = 0;


    private void Start()
    {
        transform.position     = target.position + cameraOffsetData[offsetIndex].positionOffset;

        Vector3 rotationOffset = cameraOffsetData[offsetIndex].rotationOffset;
        transform.rotation     = Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
    }

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
        {
            offsetIndex++;
            
            if(offsetIndex >= cameraOffsetData.Length)
            {
                offsetIndex = 0;
            }
        }
	}

	private void FixedUpdate()
    {
        if(!isFollowing) return;

        Vector3 targetPosition = target.position + cameraOffsetData[offsetIndex].positionOffset;
        transform.position     = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

		Vector3 rotationOffset = cameraOffsetData[offsetIndex].rotationOffset;
		transform.rotation     = Quaternion.Euler(rotationOffset.x, rotationOffset.y, rotationOffset.z);
	}

	public CameraOffsetData GetOffset()         { return cameraOffsetData[offsetIndex]; }
	public Vector3          GetPositionOffset() { return cameraOffsetData[offsetIndex].positionOffset; }
	public Vector3          GetRotationOffset() { return cameraOffsetData[offsetIndex].rotationOffset; }

	public void SetOffset(Vector3 offset)
    {
        cameraOffsetData[offsetIndex].positionOffset = offset;
    }

    public void SetRotation(Vector3 rotation)
    {
		cameraOffsetData[offsetIndex].rotationOffset = rotation;
	}
}
