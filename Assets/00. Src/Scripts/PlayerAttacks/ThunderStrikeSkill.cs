using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class ThunderStrikeSkill : PlayerAttackSkill
{
    [SerializeField] private float damageMultiplier = 10f;
    [SerializeField] private float range = 6f;
    [SerializeField] private int targetCount = 3;
    [SerializeField] private LayerMask monsterMask;
    [SerializeField] private GameObject thunderEffectPrefab;
    [SerializeField] private string effectName = "ThunderEffect";
    [SerializeField] private float attackDelay;
    private WaitForSeconds attackDelaySceconds;

    private IMultiTargetingStrategy targeting;

    private void Awake()
    {
        attackDelaySceconds = new WaitForSeconds(attackDelay);
    }

    private void Start()
    {
        targeting = new RandomNEnemyTargetingStrategy(range, targetCount, monsterMask);
    }

    public override void Attack()
    {
        StartCoroutine(Co_AttackEffect());
    }

    IEnumerator Co_AttackEffect()
    {
        Debug.Log("공격시도");
        var targets = targeting.GetTargets(transform);
        foreach (var target in targets)
        {
            GameObject obj = ObjectPool.instance.SpawnFromPool(effectName, target.position);
            StartCoroutine(Co_EffectDelay(obj));

            if (target.TryGetComponent<Monster>(out var monster))
            {
                monster.TakeDamage(damageMultiplier);
            }
            yield return attackDelaySceconds;
        }
    }

    IEnumerator Co_EffectDelay(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnToPool(effectName, obj);
    }
}
