using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAttackSkill : MonoBehaviour
{
    [SerializeField] private string skillName;
    [SerializeField] protected float baseCooldown = 1f;
    [SerializeField] protected int level = 1;

    protected float timer;

    public string GetSkillName() => skillName;

    public void Tick()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Attack();
            timer = GetCooldown();      // 레벨·버프에 따라 변동 가능
        }
    }

    public void LevelUP()
    {
        level++;
    }

    public abstract void Attack();

    public virtual float GetCooldown() => baseCooldown;
}
