using System.Collections;
using UnityEngine;

public class ExplosionDashPattern : IBossPattern
{
    private float dashSpeed;          // 대쉬 속도
    private float dashDuration;      // 대쉬 시간
    private string flameName;
    private float flameDuration;
    private float damage;

    public ExplosionDashPattern(float dashSpeed, float dashDuration, string flameName, float flameDuration, float damage)
    {
        this.dashSpeed = dashSpeed;
        this.dashDuration = dashDuration;
        this.flameName = flameName;
        this.flameDuration = flameDuration;
        this.damage = damage;
    }

    public IEnumerator Execute(BossMonster boss)
    {
        Debug.Log("불돌진 공격 실행");

        float timer = 0f;
        Rigidbody rb = boss.GetComponent<Rigidbody>();
        Vector3 direction = boss.transform.forward;

        while (timer < dashDuration)
        {
            Vector3 dashMove = direction * dashSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + dashMove);

            timer += Time.fixedDeltaTime;
            ObjectPool.instance.SpawnFromPool(flameName, boss.transform.position, boss.transform.rotation).GetComponent<FlameLineEffect>().disableTime = flameDuration;

            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f); // 후딜
    }
}
