using UnityEngine;

public class MouseLook : MonoBehaviour
{
	[SerializeField]
	private float	 rotationSpeed;

	private Plane	 plane;
	private Ray		 ray;
	private float	 distance;

	private void Start()
	{
		plane = new Plane(Vector3.up, Vector3.zero);
	}

	private void Update()
	{
		Rotate();
	}

	private void Rotate()
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if(plane.Raycast(ray, out distance))
		{
			Vector3 targetPoint = ray.GetPoint(distance);
			Vector3 direction	= (targetPoint - transform.position).normalized;

			// 너무 가까우면 회전하지 않게함.
			if(direction.sqrMagnitude > 0.001f)
			{
				Quaternion targetRotation = Quaternion.LookRotation(direction);

				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 
					Time.deltaTime * rotationSpeed);
			}
		}
	}
}
