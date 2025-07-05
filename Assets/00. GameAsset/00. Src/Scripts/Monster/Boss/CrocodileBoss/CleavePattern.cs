using System.Collections;
using UnityEngine;

public class CleavePattern : IBossPattern
{
    private string effectName;                      // 이펙트 이름
    private float damage;                           // 데미지
    private float animationDelayTime;               // 애니메이션 대기 시간
    private readonly int hashIsCleave = Animator.StringToHash("IsCleave");
    private WaitForSeconds animationDelay;

    private Animator animator;

    public CleavePattern(string effectName, float damage, float animationDelayTime , Animator animator)
    {
        this.effectName = effectName;
        this.damage = damage;
        this.animationDelayTime = animationDelayTime;
        this.animator = animator;

        animationDelay = new WaitForSeconds(animationDelayTime);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIsCleave);

        yield return animationDelay;

        Vector3 spawnPos = boss.transform.position + boss.transform.forward;
        Quaternion rotation = Quaternion.LookRotation(-boss.transform.right);

        ObjectPool.instance.SpawnFromPool(effectName, spawnPos, rotation).GetComponent<CleaveEffect>().Init(effectName, damage);

        yield return null;
    }
}
