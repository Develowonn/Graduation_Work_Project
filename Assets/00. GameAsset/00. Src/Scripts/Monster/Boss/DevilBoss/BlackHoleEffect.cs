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
    [SerializeField] private float suctionRadius = 5f;
    [SerializeField] private float suctionForce = 10f;
    [SerializeField] private LayerMask playerMask;

    private void Awake()
    {
        delay = new WaitForSeconds(returnPoolTime);
    }

    private void OnEnable()
    {
        gameObject.transform.localScale = Vector3.one;
        current = explosionTime;
        StartCoroutine(Co_Explosion());
    }

    IEnumerator Co_Explosion()
    {
        float size = current / explosionTime;

        while (current >= 0)
        {
            size = current / explosionTime;
            gameObject.transform.localScale = new Vector3(size, size, size);

            Collider playerCol = Physics.OverlapSphere(transform.position, suctionRadius * size, playerMask).FirstOrDefault();
            if (playerCol != null)
            {
                Rigidbody playerRb = playerCol.attachedRigidbody;
                if (playerRb != null)
                {
                    Vector3 dir = (transform.position - playerRb.position).normalized;
                    playerRb.AddForce(dir * suctionForce * Time.deltaTime, ForceMode.VelocityChange);
                }
            }

            current -= Time.deltaTime;
            yield return null;
        }

        GameObject exlopsionEffect = ObjectPool.instance.SpawnFromPool(shadowExplosionEffect, transform.position);
        yield return delay;
        ObjectPool.instance.ReturnToPool(shadowExplosionEffect, exlopsionEffect);
        ObjectPool.instance.ReturnToPool(blackHoleEffect, this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, suctionRadius);
    }
}
