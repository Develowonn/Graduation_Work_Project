using System.Collections;
using UnityEngine;

public class BlackHolePattern : IBossPattern
{
    private string blakHoleEffect;
    private float blackHoleSpawnDelay;
    private float range;
    private int count;

    private readonly int hashIsBlackHole = Animator.StringToHash("IsBlackHole");

    private WaitForSeconds delay;
    private Animator animator;

    public BlackHolePattern(string blakHoleEffect, float blackHoleSpawnDelay, float range, int count, Animator animator)
    {
        this.blakHoleEffect = blakHoleEffect;
        this.blackHoleSpawnDelay = blackHoleSpawnDelay;
        this.range = range;
        this.count = count;

        this.animator = animator;
        delay = new WaitForSeconds(blackHoleSpawnDelay);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * range;
            Vector3 spawnPos = boss.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            ObjectPool.instance.SpawnFromPool(blakHoleEffect, spawnPos);

            yield return delay;
        }
    }
}
