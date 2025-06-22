using UnityEngine;

public class FireBallSkillTrigger : PlayerAttackSkill
{
    [SerializeField] private float range = 6f;
    [SerializeField] private LayerMask monsterMask;

    [SerializeField] private float fireBallMoveSpeed;
    [SerializeField] private float damageMultiplier = 10f;

    private IMultiTargetingStrategy targeting;

    private void OnEnable()
    {
        targeting = new RandomEnemysTargetingStrategy(range, 1, monsterMask);
    }

    public override void Attack()
    {
        var targets = targeting.GetTargets(transform);

        foreach (var target in targets)
        {
            FireBallSkill fireBallSkill = ObjectPool.instance.SpawnFromPool(effectName, transform.position).GetComponent<FireBallSkill>();
            fireBallSkill.Init(effectName, fireBallMoveSpeed, damageMultiplier, level + 2, target);
        }
    }
}
