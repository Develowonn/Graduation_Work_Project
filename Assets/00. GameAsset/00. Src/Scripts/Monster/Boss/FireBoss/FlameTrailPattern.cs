using System.Collections;
using UnityEngine;

public class FlameTrailPattern : IBossPattern
{
    private int flameCount;                 // 생성할 화염(1열 라인) 개수
    private string flameName;               // 오브젝트 이름 (풀링)
    private float distanceBetweenFlames;    // 화염 간 거리
    private float flameDuration;            // 불 지속 시간
    private float damage;

    private WaitForSeconds waitForSeconds;
    private Animator animator;

    private readonly int hashIsMagic = Animator.StringToHash("IsMagic");

    public FlameTrailPattern(Animator animator, float flameSpawnDelay, int flameCount, string flameName, float distanceBetweenFlames, float flameDuration, float damage)
    {
        this.flameCount = flameCount;
        this.flameName = flameName;
        this.distanceBetweenFlames = distanceBetweenFlames;
        this.flameDuration = flameDuration;
        this.damage = damage;

        this.animator = animator;
        waitForSeconds = new WaitForSeconds(flameSpawnDelay);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIsMagic);

        Vector3 startPosition = boss.transform.position;                    // 시작 지점
        Vector3 forwardDirection = boss.transform.forward.normalized;       // 정면 
        Quaternion rotation = Quaternion.LookRotation(forwardDirection);    // 정면 방향

        for (int i = 0; i < flameCount; i++)
        {
            // i번째 불 장판 위치 계산
            Vector3 spawnPos = startPosition + forwardDirection * (i * distanceBetweenFlames);

            ObjectPool.instance.SpawnFromPool(flameName, spawnPos, rotation).GetComponent<FlameLineEffect>().disableTime = flameDuration;

            yield return waitForSeconds;
        }
    }
}
