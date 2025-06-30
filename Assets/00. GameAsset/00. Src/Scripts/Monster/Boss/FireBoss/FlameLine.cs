using System.Collections;
using UnityEngine;

public class FlameLine : MonoBehaviour
{
    public float disableTime = 0f;
    public string objectName;
    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(disableTime);
    }

    private void OnEnable()
    {
        StartCoroutine(WaitDisable());
    }

    IEnumerator WaitDisable()
    {
        yield return wait;
        ObjectPool.instance.ReturnToPool(objectName, gameObject);
    }
}
