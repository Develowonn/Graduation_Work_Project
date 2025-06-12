using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Monster : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed = 1.5f;
    [SerializeField] private int maxHp = 1;
    private int currentHp;

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

    public void InitPlayer(Transform playerTransform)
    {
        target = playerTransform;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position);
        direction.y = 0f; // Y�� ����
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
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ֱ� (����)
            // other.GetComponent<Player>().TakeDamage(1);

            Die(); // ������ ������ ���
        }
    }
}
