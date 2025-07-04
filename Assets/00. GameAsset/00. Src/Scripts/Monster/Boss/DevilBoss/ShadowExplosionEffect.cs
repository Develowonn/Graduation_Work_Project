using UnityEngine;
using System.Collections;

public class ShadowExplosionEffect : MonoBehaviour
{
    [SerializeField] private float explosionDelayTime;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    private WaitForSeconds explosionWait;

    private void Awake()
    {
        explosionWait = new WaitForSeconds(explosionDelayTime);
    }

    private void OnEnable()
    {
        StartCoroutine(Co_ShadowExplosion());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator Co_ShadowExplosion()
    {
        yield return explosionWait;
        Collider[] player = Physics.OverlapSphere(transform.position, range, playerLayer);
        if (player.Length > 0)
        {
            var _player = player[0].GetComponent<PlayerController>();
            if (_player != null)
            {
                _player.TakeDamage(damage);
            }
        }
    }
}
