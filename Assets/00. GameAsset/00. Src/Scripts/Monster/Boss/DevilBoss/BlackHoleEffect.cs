using System.Collections;
using System.Linq;
using UnityEngine;

public class BlackHoleEffect : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private string blackHoleEffect;
    [SerializeField] private string shadowExplosionEffect;
    [SerializeField] private float explosionTime;
    private WaitForSeconds delay;
    private float current;
    [SerializeField] private float returnPoolTime;

    [Header("Suction")]
    [SerializeField] private Transform effectTransform;
    [SerializeField] private float suctionRadius = 5f;
    [SerializeField] private float suctionForce = 10f;
    [SerializeField] private LayerMask playerMask;

    [Header("Damage")]
    [SerializeField] private float damageRadius = 1f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float damageInterval = 1f; // 데미지 간격
    private float damageTimer = 0f;

    private void Awake()
    {
        delay = new WaitForSeconds(returnPoolTime);
    }

    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.one;
        current = explosionTime;
        damageTimer = 0f;
        StartCoroutine(Co_Explosion());
    }

    IEnumerator Co_Explosion()
    {
        float size;

        while (current >= 0)
        {
            size = current / explosionTime;
            gameObject.transform.localScale = new Vector3(size, size, size);

            Collider playerCol = Physics.OverlapSphere(effectTransform.position, suctionRadius * size, playerMask).FirstOrDefault();
            if (playerCol != null)
            {
                Rigidbody playerRb = playerCol.attachedRigidbody;
                if (playerRb != null)
                {
                    Vector3 dir = (effectTransform.position - playerRb.position).normalized;
                    playerRb.AddForce(dir * suctionForce * Time.deltaTime, ForceMode.VelocityChange);
                }

                float distance = Vector3.Distance(effectTransform.position, playerCol.transform.position);
                if (distance < damageRadius)
                {
                    damageTimer -= Time.deltaTime;
                    if (damageTimer <= 0f)
                    {
                        PlayerController player = playerCol.GetComponent<PlayerController>();
                        if (player != null)
                        {
                            player.TakeDamage(damage);
                            damageTimer = damageInterval;
                        }
                    }
                }
                else
                {
                    damageTimer = 0f; // 다시 들어오면 바로 맞도록 초기화
                }
            }

            current -= Time.deltaTime;
            yield return null;
        }

        GameObject explosionEffect = ObjectPool.instance.SpawnFromPool(shadowExplosionEffect, effectTransform.position);
        yield return delay;
        ObjectPool.instance.ReturnToPool(shadowExplosionEffect, explosionEffect);
        ObjectPool.instance.ReturnToPool(blackHoleEffect, this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, suctionRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
