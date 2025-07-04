using System.Collections;
using UnityEngine;

public class BeamRainPattern : IBossPattern
{
    private float range;
    private string effectName;
    private int count;
    private float delayTime;

    private readonly int hashIsBeamRain = Animator.StringToHash("IsBeamRain");

    private Animator animator;
    private WaitForSeconds delay;

    public BeamRainPattern(float range, string effectName, int count, float delayTime, Animator animator)
    {
        this.range = range;
        this.effectName = effectName;
        this.count = count;
        this.delayTime = delayTime;

        this.animator = animator;
        delay = new WaitForSeconds(delayTime);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIsBeamRain);

        for (int i = 0; i < count; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * range;
            Vector3 spawnPos = boss.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            ObjectPool.instance.SpawnFromPool(effectName, spawnPos);

            yield return delay;
        }
    }
}
