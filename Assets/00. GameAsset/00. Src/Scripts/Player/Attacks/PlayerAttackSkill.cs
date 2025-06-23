using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public abstract class PlayerAttackSkill : MonoBehaviour
{
    [SerializeField] private string skillName;                                      // 스킬 이름
    [SerializeField] protected float baseCooldown = 1f;                             // 스킬 기본 쿨타임
    [SerializeField] protected int level = 1;                                       // 스킬 레벨
    [SerializeField] protected string effectName = "ThunderEffect";                 // 스킬 이펙트 이름 (Pool에서 가져올 용도)
    [SerializeField] protected float damage;                                        // 스킬 기본 데미지

    protected float timer;

    /// <summary>
    /// 스킬 이름 반환 함수
    /// </summary>
    /// <returns></returns>
    public string GetSkillName() => skillName;

    /// <summary>
    /// 스킬 쿨타임 함수
    /// </summary>
    public void Tick()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            Attack();
            timer = GetCooldown();      // 레벨·버프에 따라 변동 가능
        }
    }

    /// <summary>
    /// 스킬 레벨업
    /// </summary>
    public void UPLevel()
    {
        level++;
    }

    /// <summary>
    /// 스킬 공격 실행 함수
    /// </summary>
    public abstract void Attack();

    /// <summary>
    /// 스킬 쿨타임 반환 함수
    /// </summary>
    /// <returns></returns>
    public virtual float GetCooldown() => baseCooldown;
}
