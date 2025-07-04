using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

// 번개 스킬
public class ThunderStrikeSkillTrigger : PlayerAttackSkill
{
    [SerializeField] private float range = 6f;                          // 범위
    [SerializeField] private int targetCount = 3;                       // 공격할 몬스터 마리수
    [SerializeField] private LayerMask monsterMask;                     // 몬스터 레이어
    [SerializeField] private float attackDelay;                         // 1회 공격 후 딜레이 시간
    private WaitForSeconds attackDelaySceconds;

    private IMultiTargetingStrategy targeting;                          // 멀티 타겟팅

    private void Awake()
    {
        attackDelaySceconds = new WaitForSeconds(attackDelay);
    }

    private void OnEnable()
    {
        targeting = new RandomEnemysTargetingStrategy(range, targetCount, monsterMask);
    }

    public override void Attack()
    {
        StartCoroutine(Co_AttackEffect());
    }

    /// <summary>
    /// 스킬 시전 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator Co_AttackEffect()
    {
        Debug.Log("공격시도");
        (targeting as RandomEnemysTargetingStrategy).Init(range, targetCount + level, monsterMask);
        var targets = targeting.GetTargets(transform);

        if (targets != null || targets.Count > 0)      // 예외 처리
        {
            foreach (var target in targets)
            {
                if (target != null)         // 예외 처리
                {
                    GameObject obj = ObjectPool.instance.SpawnFromPool(effectName, target.position);
                    StartCoroutine(Co_EffectDelay(obj));

                    if (target.TryGetComponent<Monster>(out var monster))
                    {
                        monster.TakeDamage(GetAttackPower());
                    }
                    yield return attackDelaySceconds;
                }
            }
        }
    }

    /// <summary>
    /// 일정 시간 후 이펙트 pool로 리턴 코루틴
    /// </summary>
    /// <param name="obj">리턴할 오브젝트</param>
    /// <returns></returns>
    IEnumerator Co_EffectDelay(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnToPool(effectName, obj);
    }
}
