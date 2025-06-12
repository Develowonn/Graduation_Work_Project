using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Monster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private float maxHp = 1;
    private float currentHp;
    private string monsterName;
    [SerializeField] private float dropExp = 10f;

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
        direction.y = 0f; // Y�� ����
        direction = direction.normalized;

        Vector3 nextPosition = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + nextPosition);
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        StageManager.instance.MonsterDie(dropExp);
        ObjectPool.instance.ReturnToPool(monsterName, gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ֱ� (����)
            // other.GetComponent<Player>().TakeDamage(1);

            // Die(); // ������ ������ ���
        }
    }
}
