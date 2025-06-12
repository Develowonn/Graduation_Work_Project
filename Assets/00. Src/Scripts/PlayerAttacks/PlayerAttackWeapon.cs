using UnityEngine;

[System.Serializable]
public abstract class PlayerAttackWeapon : MonoBehaviour
{
    [SerializeField] protected float baseCooldown = 1f;
    [SerializeField] protected int level = 1;

    protected float timer;

    public void Tick()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Attack();
            timer = GetCooldown();      // 레벨·버프에 따라 변동 가능
        }
    }

    public abstract void Attack();

    public virtual float GetCooldown() => baseCooldown;
}
