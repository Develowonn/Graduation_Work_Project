using System.Collections;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    [Header("Pooling")]
    [SerializeField] private string objectName;
    [SerializeField] private float waitTime = 1f;
    private WaitForSeconds wait;

    [Header("Damage")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask targetMask;  

    private void Awake()
    {
        wait = new WaitForSeconds(waitTime);
    }

    private void OnEnable()
    {
        DamageNearbyPlayers();
        StartCoroutine(Co_WaitReturnToPool());
    }

    private void DamageNearbyPlayers()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius, targetMask);

        if (targets.Length <= 0) return;
        foreach (Collider target in targets)
        {
            PlayerController player = target.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }

    IEnumerator Co_WaitReturnToPool()
    {
        yield return wait;
        ObjectPool.instance.ReturnToPool(objectName, gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
