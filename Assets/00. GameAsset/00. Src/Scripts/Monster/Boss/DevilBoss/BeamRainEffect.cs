using System.Collections;
using UnityEngine;

public class BeamRainEffect : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float damageDelayTime;
    [SerializeField] private LayerMask playerLayer;
    private WaitForSeconds damageDelay;

    private void Awake()
    {
        damageDelay = new WaitForSeconds(damageDelayTime);
    }

    private void OnEnable()
    {
        StartCoroutine(Co_BeamDamage());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    IEnumerator Co_BeamDamage()
    {
        yield return damageDelay;
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
