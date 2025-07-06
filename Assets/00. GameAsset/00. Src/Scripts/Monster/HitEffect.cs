using System.Collections;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private float disableDelayTime;
    [SerializeField] private string effectName;
    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(disableDelayTime);
    }

    private void OnEnable()
    {
        StartCoroutine(Co_DisableDelay());
    }

    IEnumerator Co_DisableDelay()
    {
        yield return wait;
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }
}
