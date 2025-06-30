// # System
using System;
using System.Collections.Generic;

// # Unity
using UnityEngine;

// # Etc
using Cysharp.Threading.Tasks;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField]
	private TextAsset			   monsterSpawnDataJson;
	[SerializeField]
	private List<MonsterSpawnData> monsterSpawnDatas;

	private decimal				   elapsedTime;

	private GameObject			   player;
 
	private void Start()
	{
		player = InGameManager.Instance.GetPlayerObject();

		InitializeMonsterSpawnData();

		SpawnMonster().Forget();
	}

	private void InitializeMonsterSpawnData()
	{
		MonsterSpawnDataList monsterSpawnDataList = JsonUtility.FromJson<MonsterSpawnDataList>(monsterSpawnDataJson.text);

	    monsterSpawnDatas						  = new List<MonsterSpawnData>(); 
		monsterSpawnDatas						  = monsterSpawnDataList.monsterSpawnDatas;
		monsterSpawnDatas.Sort((x, y) => x.spawnTime.CompareTo(y.spawnTime));
	}

	private async UniTaskVoid SpawnMonster()
	{
		while (true)
		{
			foreach(var monsterSpawnData in monsterSpawnDatas)
			{
				if(monsterSpawnData.spawnTime != elapsedTime)
					break;

				Vector3 spawnPos = player.transform.position + monsterSpawnData.spawnPosition;
				Monster monster = ObjectPool.instance.SpawnFromPool(monsterSpawnData.monsterName, spawnPos).GetComponent<Monster>();
				monster.InitMonster(StageManager.instance.GetPlayerObject().transform, monsterSpawnData.monsterName);
			}

			elapsedTime += 1.0m;
			await UniTask.Delay(TimeSpan.FromSeconds(1.0f));
		}
	}
}
