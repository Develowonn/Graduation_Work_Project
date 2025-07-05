using UnityEngine;
using System.Collections;

public class CleaveEffect : MonoBehaviour
{
    [SerializeField] private string effectName;
    [SerializeField] private float damage;
    [SerializeField] private float returnPoolTime;
    private WaitForSeconds returnPoolDelay;

    private void Awake()
    {
        returnPoolDelay = new WaitForSeconds(returnPoolTime);
    }

    public void Init(string effectName, float damage)
    {
        this.effectName = effectName;
        this.damage = damage;
    }

    private void OnEnable()
    {
        StartCoroutine(Co_ReturnPool());
    }

    IEnumerator Co_ReturnPool()
    {
        yield return null;
        yield return returnPoolDelay;
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
