using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public class HealingObjetSpawner : MonoBehaviour
{
	[SerializeField] 
	private GameObject  healingObjectPrefab;
	[SerializeField] 
	private Transform	playerTransform;
	[SerializeField] 
	private float		spawnInterval;
	[SerializeField] 
	private float		spawnRadius;
	[SerializeField] 
	private float		heightOffset;

	private void Start()
	{
		StartSpawnLoop().Forget();
	}

	private async UniTaskVoid StartSpawnLoop()
	{
		while (true)
		{
			await UniTask.Delay(TimeSpan.FromSeconds(spawnInterval));

			Vector2 randomCircle = UnityEngine.Random.insideUnitCircle * spawnRadius;
			Vector3 targetPos    = playerTransform.position + new Vector3(randomCircle.x, 0.5f, randomCircle.y);
			Vector3 spawnPos     = targetPos + Vector3.up * heightOffset;

			GameObject orb = Instantiate(healingObjectPrefab, spawnPos, Quaternion.identity);
			orb.GetComponent<HealingObject>().Init(targetPos, 2f, 1f);
		}
	}
}