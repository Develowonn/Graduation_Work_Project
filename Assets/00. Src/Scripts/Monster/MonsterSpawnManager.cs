using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float spawnDelay = 1.0f;
    private WaitForSeconds spawnCoroutineDelaySeconds;
    private WaitForSeconds spawnDelaySeconds;
    [SerializeField] List<string> monsterNameList = new List<string>();
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
    }

    private IEnumerator SpawnMonster()
    {
        int count = Random.Range(minSpawnMonsterCount, maxSpawnMonsterCount);
        for (int i = 0; i < count; i++)
        {
            float angle = Random.Range(0f, 360f);
            float distance = Random.Range(minDistance, maxDistance);

            // ȸ�� ���� ���� ��� (XZ ��� ����)
            Vector3 direction = Quaternion.Euler(0, angle, 0) * Vector3.forward;
            Vector3 spawnPosition = playerPosition.position + direction * distance;

            // ����(y)�� �÷��̾� �������� �����ϰų�, ���� ���� ����
            spawnPosition.y = playerPosition.position.y;
            ObjectPool.instance._SpawnFromPool("Monster", spawnPosition);
            yield return spawnDelaySeconds;
        }
        yield return spawnDelaySeconds;
        StartCoroutine(SpawnMonster());
    }
}
