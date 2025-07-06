using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class Monster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float moveSpeed = 1.5f;          // 이동속도
    [SerializeField] protected float maxHp = 1;                 // 최대 체력
    protected float currentHp;                                  // 현재 체력
    protected string monsterName;                               // 몬스터 이름
    [SerializeField] protected float dropExp = 10f;             // 처치 시 경험치 드랍량
    [SerializeField] protected float collisionDamage;           // 부딪혔으 때 데미지
    protected bool isMoveStop = false;                          // 움직임 정지

    protected Transform target;
    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// 몬스터 초기화 함수
    /// </summary>
    /// <param name="playerTransform">따라갈 타겟</param>
    /// <param name="monsterName">몬스터 이름(풀링용)</param>
    public void InitMonster(Transform playerTransform, string monsterName)
    {
        this.target = playerTransform;
        this.monsterName = monsterName;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        if (!isMoveStop) Move();
    }

    protected void Move()
    {
        Vector3 direction = (target.position - transform.position);
        direction.y = 0f; // Y축 고정
        direction = direction.normalized;

        Vector3 nextPosition = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextPosition);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public float GetAttackPower()
    {
        return collisionDamage;
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("HIT! Damage : " + damage);
        currentHp -= damage;
        if (currentHp <= 0)
        {
            InGameManager.Instance.IncreaseCaughtMonsterCount();
			Die();
		}
	}

    private void Die()
    {
        StageManager.instance.DieMonster(dropExp);
        SoundManager.Instance.PlaySFX("적 처치");
        ObjectPool.instance.ReturnToPool(monsterName, gameObject);
    }
}