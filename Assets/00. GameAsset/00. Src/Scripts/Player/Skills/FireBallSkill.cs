using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private string effectName;                 // 이펙트 이름 ( 풀로 리턴하는 용도 )
    [SerializeField] private float moveSpeed;                   // 움직임 속도
    [SerializeField] private float attackPower;                 // 기본 데미지
    [SerializeField] private int duration;                      // 지속시간

    private Vector3 moveDirection;                              // 움직일 방향

    /// <summary>
    /// 화염구 초기화 함수
    /// </summary>
    /// <param name="effectName">스킬 이펙트 이름</param>
    /// <param name="moveSpeed">움직임 속도</param>
    /// <param name="attackPower">기본 데미지</param>
    /// <param name="duration">지속 시간</param>
    /// <param name="enemyTrans">방향</param>
    public void Init(string effectName, float moveSpeed, float attackPower, int duration, Transform enemyTrans)
    {
        this.effectName = effectName;
        this.moveSpeed = moveSpeed;
        this.attackPower = attackPower;
        this.duration = duration + 2;

        Vector3 dir = enemyTrans.position - transform.position;
        dir.y = 0f;
        moveDirection = dir.normalized;

        StartCoroutine(Co_ObjectOff());
    }

    private void FixedUpdate()
    {
        MoveFireBall();
    }

    /// <summary>
    /// 화염구 이동 함수
    /// </summary>
    private void MoveFireBall()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster")) // 몬스터에 닿았을 때 피격하도록
        {
            Monster monster = other.GetComponent<Monster>();

            monster.TakeDamage(attackPower);
        }
    }

    /// <summary>
    /// 지속 시간 후 pool로 리턴
    /// </summary>
    /// <returns></returns>
    IEnumerator Co_ObjectOff()
    {
        yield return new WaitForSeconds(duration);
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }
}
