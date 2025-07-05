using System.Collections;
using UnityEngine;

public class SpikeEffect : MonoBehaviour
{
    [SerializeField] private string effectName; 
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float returnPoolTime;
    private WaitForSeconds returnPoolDelay;

    private void Awake()
    {
        returnPoolDelay = new WaitForSeconds(returnPoolTime);
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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Init(string effectName, float damage)
    {
        this.effectName = effectName;
        this.damage = damage;
    }

    IEnumerator Co_BeamDamage()
    {
        yield return null;
        Collider[] player = Physics.OverlapSphere(transform.position, range, playerLayer);
        if (player.Length > 0)
        {
            var _player = player[0].GetComponent<PlayerController>();
            if (_player != null)
            {
                _player.TakeDamage(damage);
            }
        }

        yield return returnPoolDelay;
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }
}
