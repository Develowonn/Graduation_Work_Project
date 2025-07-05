using System.Collections;
using UnityEngine;

public class GroundSpikePattern : IBossPattern
{
    private string mainEffectName;              // 메인 Spike 이펙트
    private string subEffectName;               // 서브 Spike 이펙트
    private float subSpikeDelayTime;            // 폭발 당 딜레이
    private float mainSpikeDamage;              // 메인 Spike 공격력
    private float subSpikeDamage;               // 서브 Spike 데미지
    private float subEffectRange;               // 서브 스파이크 복발 범위
    private float subEffectCount;               // 서브 스파이크 개수
    private float delayAfterMain;               // 메인 스파이크 이후 딜레이

    private WaitForSeconds mainSpikeDelay;
    private WaitForSeconds subSpikeDelay;

    private readonly int hashIGroundSpike = Animator.StringToHash("IsGroundSpike");

    private Animator animator;

    public GroundSpikePattern(string mainEffectName, string subEffectName, float subSpikeDelayTime, float mainSpikeDamage, float subSpikeDamage, float subEffectRange, float subEffectCount, float delayAfterMain, Animator animator)
    {
        this.mainEffectName = mainEffectName;
        this.subEffectName = subEffectName;
        this.subSpikeDelayTime = subSpikeDelayTime;
        this.mainSpikeDamage = mainSpikeDamage;
        this.subSpikeDamage = subSpikeDamage;
        this.subEffectRange = subEffectRange;
        this.subEffectCount = subEffectCount;
        this.delayAfterMain = delayAfterMain;
        this.animator = animator;

        mainSpikeDelay = new WaitForSeconds(delayAfterMain);
        subSpikeDelay = new WaitForSeconds(subSpikeDelayTime);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIGroundSpike);

        yield return new WaitForSeconds(0.5f);

        ObjectPool.instance.SpawnFromPool(mainEffectName, boss.transform.position).GetComponent<SpikeEffect>().Init(mainEffectName, mainSpikeDamage);

        yield return mainSpikeDelay;

        for (int i = 0; i < subEffectCount; i++)
        {
            Vector2 randomCircle = Random.insideUnitCircle * subEffectRange;
            Vector3 spawnPos = boss.transform.position + new Vector3(randomCircle.x, 0f, randomCircle.y);

            ObjectPool.instance.SpawnFromPool(subEffectName, spawnPos).GetComponent<SpikeEffect>().Init(subEffectName, subSpikeDamage);

            yield return subSpikeDelay;
        }
    }
}
