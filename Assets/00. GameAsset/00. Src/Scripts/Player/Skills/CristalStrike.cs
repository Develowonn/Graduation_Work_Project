using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CristalStrike : PlayerAttackSkill
{
    [Header("Stat")]
    public float rage;                                      // 범위
    [SerializeField] private LayerMask monsterMask;         // 몬스터 레이어

    public override void Attack()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, rage + (level * (rage * 10)), monsterMask);
        GameObject skillObject = ObjectPool.instance.SpawnFromPool(effectName, transform.position, 0.5f + (level * 0.1f));
        StartCoroutine(Co_EffectDelay(skillObject));
        Debug.Log($"감지된 몬스터 수: {objects.Length}");
        foreach (Collider obj in objects)
        {
            Monster monster = obj.GetComponent<Monster>();
            monster.TakeDamage(damage);
        }
    }

    IEnumerator Co_EffectDelay(GameObject obj)
    {
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.ReturnToPool(effectName, obj);
    }
}