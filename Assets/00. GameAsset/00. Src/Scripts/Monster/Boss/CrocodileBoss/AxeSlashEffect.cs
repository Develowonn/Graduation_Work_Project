using System.Collections;
using UnityEngine;

public class AxeSlashEffect : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private Vector3 moveDirection;
    private float damage;
    private float duration;
    private string effectName;

    private void OnEnable()
    {
        StartCoroutine(Co_ReturnPool());
    }

    private void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            player.TakeDamage(damage);
        }
    }

    IEnumerator Co_ReturnPool()
    {
        yield return null;  
        yield return new WaitForSeconds(duration);
        ObjectPool.instance.ReturnToPool(effectName, gameObject);
    }

    /// <summary>
    /// 방향과 속도를 설정하는 초기화 함수
    /// </summary>
    /// <param name="direction">정규화된 방향 벡터</param>
    /// <param name="speed">이동 속도</param>
    public void Init(Vector3 direction, float speed, float damage, float duration, string effectName)
    {
        this.moveDirection = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.duration = duration;
        this.effectName = effectName;
    }
}
