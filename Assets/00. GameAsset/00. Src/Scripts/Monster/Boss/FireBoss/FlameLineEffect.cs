using System.Collections;
using System.Linq;
using UnityEngine;

public class FlameLineEffect : MonoBehaviour
{
    [Header("HitBox Size")]
    [SerializeField] private Vector3 hitBoxSize = new Vector3(1f, 1f, 1f);          // 박스 사이즈

    [Header("Damage")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private float hitInterval = 0.5f;

    [Header("ReturnPool")]
    public float disableTime = 0f;
    public string objectName;

    private Coroutine hitCoroutine;
    private static readonly Collider[] results = new Collider[1];
    private WaitForSeconds interval;

    private void Awake()
    {
        interval = new WaitForSeconds(hitInterval);
    }

    private void OnEnable()
    {
        hitCoroutine = StartCoroutine(HitLoop());   // 공격 시작
        StartCoroutine(WaitDisable());
    }

    private void OnDisable()
    {
        if (hitCoroutine != null)
        {
            StopCoroutine(hitCoroutine);            // 공격 종료
            hitCoroutine = null;
        }
    }

    IEnumerator WaitDisable()
    {
        yield return null;
        yield return new WaitForSeconds(disableTime);
        ObjectPool.instance.ReturnToPool(objectName, gameObject);
    }
    
    /// <summary>
    /// 공격 주기 반복
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitLoop()
    {
        Vector3 halfExtents = hitBoxSize * 0.5f;

        while (true)
        {
            int count = Physics.OverlapBoxNonAlloc(
                transform.position,
                halfExtents,
                results,
                transform.rotation,
                targetMask
            );

            if (count > 0)
            {
                PlayerController player = results[0].GetComponent<PlayerController>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
            }

            yield return interval;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Matrix4x4 matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.matrix = matrix;

        Gizmos.DrawWireCube(Vector3.zero, hitBoxSize);
    }
}
