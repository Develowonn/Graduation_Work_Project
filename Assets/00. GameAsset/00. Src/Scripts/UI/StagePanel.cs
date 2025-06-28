using UnityEngine;

public class StagePanel : MonoBehaviour
{
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Utils.Dotween.PlayScaleAnimation(transform, Vector3.zero, 0.3f, () => { gameObject.SetActive(false); });
		}
	}
}
