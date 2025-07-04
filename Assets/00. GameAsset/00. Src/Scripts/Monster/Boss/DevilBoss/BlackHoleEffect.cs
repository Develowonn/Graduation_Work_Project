using System.Collections;
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
        while(current >= 0)
        {
            size = current / explosionTime;
            gameObject.transform.localScale = new Vector3(size, size, size);
            Debug.Log(size);
            current -= Time.deltaTime;
            yield return null;
        }

        GameObject exlopsionEffect = ObjectPool.instance.SpawnFromPool(shadowExplosionEffect, transform.position);
        yield return delay;
        ObjectPool.instance.ReturnToPool(shadowExplosionEffect, exlopsionEffect);
        ObjectPool.instance.ReturnToPool(blackHoleEffect, this.gameObject);
    }
}
