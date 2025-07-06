using UnityEngine;

public class SkullExplosionEffect : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float delayTime;
    private WaitForSeconds delay;

    private void Awake()
    {
        delay = new WaitForSeconds(delayTime);
    }


}
