using System.Collections;
using UnityEngine;

public class AxeSlashPattern : IBossPattern
{
    private string effectName;              // 이펙트 이름 (풀링)
    private int count;                      // 슬레쉬 이펙트 개수
    private float slashDuration;            // 슬레쉬 지속시간
    private float damage;                   // 데미지  
    private float slashAttackDelay;         // 슬레쉬 한번당 딜레이
    private float slashSpeed;               // 슬레쉬 속도
    private WaitForSeconds delay;

    private readonly int hashIsSlash = Animator.StringToHash("IsSlash");

    private Animator animator;
    private Transform target;

    public AxeSlashPattern(string effectName, int count, float slashDuration, float damage, float slashAttackDelay, float slashSpeed, Animator animator, Transform target)
    {
        this.effectName = effectName;
        this.count = count;
        this.slashDuration = slashDuration;
        this.damage = damage;
        this.slashAttackDelay = slashAttackDelay;
        this.slashSpeed = slashSpeed;

        this.animator = animator;
        this.target = target;

        delay = new WaitForSeconds(slashAttackDelay);
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIsSlash);
        for (int i = 0; i < count; i++)
        {

            yield return new WaitForSeconds(0.2f);

            Vector3 direction = (target.position - boss.transform.position).normalized;

            Vector3 spawnPos = boss.transform.position + direction * 1.5f;

            GameObject effectObj = ObjectPool.instance.SpawnFromPool(effectName, spawnPos);
            AxeSlashEffect effect = effectObj.GetComponent<AxeSlashEffect>();
            if (effect != null)
            {
                effect.Init(direction, slashSpeed, damage, slashDuration, effectName);
            }

            yield return delay;
        }
    }
}
