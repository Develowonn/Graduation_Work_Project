using System.Collections;
using UnityEngine;

public class FlameTrailPattern : IBossPattern
{
    private int flameCount;                 // 생성할 화염(1열 라인) 개수
    private string flameName;               // 오브젝트 이름 (풀링)
    private float distanceBetweenFlames;    // 화염 간 거리
    private float flameDuration;

    private WaitForSeconds waitForSeconds;

    public FlameTrailPattern(float flameSpawnDelay, int flameCount, string flameName, float distanceBetweenFlames, float flameDuration)
    {
        this.flameCount = flameCount;
        this.flameName = flameName;
        this.distanceBetweenFlames = distanceBetweenFlames;
        this.flameDuration = flameDuration;

        waitForSeconds = new WaitForSeconds(flameSpawnDelay);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        Vector3 startPosition = boss.transform.position;                    // 시작 지점
        Vector3 forwardDirection = boss.transform.forward.normalized;       // 정면 
        Quaternion rotation = Quaternion.LookRotation(forwardDirection);    // 정면 방향

        for (int i = 0; i < flameCount; i++)
        {
            // i번째 불 장판 위치 계산
            Vector3 spawnPos = startPosition + forwardDirection * (i * distanceBetweenFlames);

            ObjectPool.instance.SpawnFromPool(flameName, spawnPos, rotation).GetComponent<FlameLine>().disableTime = flameDuration;

            yield return waitForSeconds;
        }
    }
}
