using System.Collections;
using UnityEngine;

public class SkullExplosion : IBossPattern
{
    private string chargingEffect;
    private string skullEffect;
    private Transform skillPos;

    private readonly int hashIsCharging = Animator.StringToHash("IsCharging");

    private Animator animator;

    public SkullExplosion(string chargingEffect, string skullEffect, Transform skillPos, Animator animator)
    {
        this.chargingEffect = chargingEffect;
        this.skullEffect = skullEffect;
        this.skillPos = skillPos;
        this.animator = animator;
    }

    public IEnumerator Execute(BossMonster boss)
    {
        animator.SetTrigger(hashIsCharging);
        ObjectPool.instance.SpawnFromPool(chargingEffect, skillPos.position);
        yield return new WaitForSeconds(2.5f);
        ObjectPool.instance.SpawnFromPool(skullEffect, skillPos.position);

        yield return new WaitForSeconds(3f);
    }
}
