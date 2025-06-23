using UnityEngine;

// 화염구 스킬 트리거
public class FireBallSkillTrigger : PlayerAttackSkill
{
    [SerializeField] private float range = 6f;                  // 범위
    [SerializeField] private LayerMask monsterMask;             // 몬스터 레이어

    [SerializeField] private float fireBallMoveSpeed;           // 화염구 이동속도

    private IMultiTargetingStrategy targeting;                  // 타겟팅

    private void OnEnable()
    {
        targeting = new RandomEnemysTargetingStrategy(range, 1, monsterMask);
    }

    public override void Attack()
    {
        var targets = targeting.GetTargets(transform);

        foreach (var target in targets)
        {
            Debug.Log("화염구 발사");
            FireBallSkill fireBallSkill = ObjectPool.instance.SpawnFromPool(effectName, transform.position).GetComponent<FireBallSkill>();
            fireBallSkill.Init(effectName, fireBallMoveSpeed, damage, level, target);
        }
    }
}
