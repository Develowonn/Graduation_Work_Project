using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseLook : MonoBehaviour
{
	[SerializeField]
	private float	 rotationSpeed;
	[SerializeField]
	private float	 minMouseDistance;

	private Plane	 plane;
	private Ray		 ray;
	private float	 distance;

	private void Start()
	{
		plane = new Plane(Vector3.up, Vector3.zero);
	}

	private void Update()
	{
		if(StageManager.instance.GetCurrentGameState() == InGameState.end)
		{
			return;
		}

		Rotate();
	}

	private void Rotate()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (plane.Raycast(ray, out distance))
		{
			Vector3 targetPoint = ray.GetPoint(distance);
			Vector3 toTarget    = targetPoint - transform.position;

			if (toTarget.sqrMagnitude < minMouseDistance)
				return;

			Vector3    direction      = toTarget.normalized;
			Quaternion targetRotation = Quaternion.LookRotation(direction);

			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,
				Time.deltaTime * rotationSpeed);
		}
	}
}