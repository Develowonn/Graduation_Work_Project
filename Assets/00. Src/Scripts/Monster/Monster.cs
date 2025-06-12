using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Monster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private int maxHp = 1;
    private int currentHp;
    private string monsterName;

    private Transform target;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        currentHp = maxHp;
    }

    public void InitMonster(Transform playerTransform, string monsterName)
    {
        this.target = playerTransform;
        this.monsterName = monsterName;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position);
        direction.y = 0f; // Y축 고정
        direction = direction.normalized;

        Vector3 nextPosition = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextPosition);
        
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        ObjectPool.instance.ReturnToPool(monsterName, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지 주기 (예시)
            // other.GetComponent<Player>().TakeDamage(1);

            // Die(); // 자폭형 몬스터인 경우
        }
    }
}
