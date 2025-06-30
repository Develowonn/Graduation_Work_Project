// # System
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
	[SerializeField]
	private TextAsset			   monsterSpawnDataJson;

	private List<MonsterSpawnData> monsterSpawnDatas;

	private void Start()
	{
		InitializeMonsterSpawnData();
	}

	private void InitializeMonsterSpawnData()
	{
		MonsterSpawnDataList monsterSpawnDataList = JsonUtility.FromJson<MonsterSpawnDataList>(monsterSpawnDataJson.text);

	    monsterSpawnDatas						  = new List<MonsterSpawnData>(); 
		monsterSpawnDatas						  = monsterSpawnDataList.monsterSpawnDatas;
	}
}
