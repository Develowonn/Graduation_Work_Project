using DG.Tweening;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 1.0f)]
	private float	healingPercent;
	[SerializeField]
	private float	destroyTime;

	private void Start()
	{
		Destroy(gameObject, destroyTime);
	}

	public void Init(Vector3 targetPosition, float jumpPower = 2f, float duration = 1f)
	{
		transform.DOJump(targetPosition, jumpPower, 1, duration)
				 .SetEase(Ease.OutQuad);
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerStat playerStat = other.GetComponent<PlayerStat>();

		if (playerStat != null)
		{
			playerStat.IncreaseHPByPercent(healingPercent);
			Destroy(gameObject);
		}
	}
}
