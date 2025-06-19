using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallSkill : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private string effectName;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float attackPower;
    [SerializeField] private int penetrateCount;

    private Vector3 moveDirection;

    private HashSet<Monster> hitMonsters = new HashSet<Monster>();

    private void OnEnable()
    {
        hitMonsters.Clear();
        StartCoroutine(Co_ObjectOff());
    }

    public void Init(string effectName, float moveSpeed, float attackPower, int hp, Transform enemyTrans)
    {
        this.effectName = effectName;
        this.moveSpeed = moveSpeed;
        this.attackPower = attackPower;

        Vector3 dir = enemyTrans.position - transform.position;
        dir.y = 0f;
        moveDirection = dir.normalized;
    }

    private void FixedUpdate()
    {
        MoveFireBall();
    }

    private void MoveFireBall()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Monster monster = other.GetComponent<Monster>();

            // 이미 맞은 몬스터는 무시 
            //if (hitMonsters.Contains(monster)) return;

            monster.TakeDamage(attackPower);
            //hitMonsters.Add(monster);

            //penetrateCount -= 1;
            //if (penetrateCount <= 0)
            //{
            //    ObjectPool.instance.ReturnToPool(effectName, gameObject);
            //}
        }
    }

    IEnumerator Co_ObjectOff()
    {
        yield return new WaitForSeconds(3f);
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }
}
