using System.Collections;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TimeMonsterIndex
{
    public List<string> monsterNameList;
    public float time;
}

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float spawnDelay = 1.0f;
    private WaitForSeconds spawnCoroutineDelaySeconds;
    private WaitForSeconds spawnDelaySeconds;
    [SerializeField] List<TimeMonsterIndex> monsterList = new List<TimeMonsterIndex>();
    [SerializeField] private int minSpawnMonsterCount = 0;
    [SerializeField] private int maxSpawnMonsterCount = 0;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;

    private void Awake()
    {
        spawnCoroutineDelaySeconds = new WaitForSeconds(spawnDelay);
        spawnDelaySeconds = new WaitForSeconds(0.1f);
    }

    private void Start()
    {
        StartCoroutine(SpawnMonster());
        TimeManager.instance.AddBossWaveAction(SpawnBossMonster);
    }

    private IEnumerator SpawnMonster()
    {
        while (true)
        {
            int count = Random.Range(minSpawnMonsterCount, maxSpawnMonsterCount);
            for (int i = 0; i < count; i++)
            {
                float angle = Random.Range(0f, 360f);
                float distance = Random.Range(minDistance, maxDistance);

                Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
                Vector3 spawnPosition = playerPosition.position + direction * distance;

                spawnPosition.y = playerPosition.position.y;
                TimeMonsterIndex monsterList = GetMonsterIndex();
                string monsterName = monsterList.monsterNameList[Random.Range(0, monsterList.monsterNameList.Count - 1)];

                SpawnMonster(monsterName, spawnPosition);

                yield return spawnDelaySeconds;
            }

            yield return spawnCoroutineDelaySeconds; // 다음 웨이브까지 대기
        }
    }

    private void SpawnMonster (string monsterName, Vector3 spawnPosition)
    {
        ObjectPool.instance.SpawnFromPool(monsterName, spawnPosition).GetComponent<Monster>().InitMonster(playerPosition, monsterName);
    }

    public void SpawnBossMonster()
    {
        string monsterName = StageManager.instance.GetBossMonster();

        if (monsterName == null) return;

        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
        Vector3 spawnPosition = playerPosition.position + direction * distance;

        spawnPosition.y = playerPosition.position.y;

        ObjectPool.instance.SpawnFromPool(monsterName, spawnPosition).GetComponent<Monster>().InitMonster(playerPosition, monsterName);
    }

    private TimeMonsterIndex GetMonsterIndex()
    {
        foreach (var index in monsterList)
        {
            if (TimeManager.instance.GetCurrentTime() > index.time) return index;
        }

        return monsterList[monsterList.Count - 1];
    }
}
