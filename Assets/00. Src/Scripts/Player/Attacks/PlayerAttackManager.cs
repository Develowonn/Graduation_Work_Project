using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private readonly List<PlayerAttackSkill> attackSkillList = new();

    public void EquipWeapon(PlayerAttackSkill skillPrefab)
    {
        attackSkillList.Add(skillPrefab);
    }

    public void UnequipWeapon(PlayerAttackSkill skill)
    {
        if (attackSkillList.Remove(skill))
            Destroy(skill.gameObject);
    }

    public void GetOrLevelUpSkill(SkillSO skillData)
    {
        bool haveSkill = false;
        foreach (var s in attackSkillList)
        {
            if(skillData.skillName == s.GetSkillName()) haveSkill = true;
        }

        if (haveSkill) // 스킬이 있다면
        {
            skillData.LevelUpSkill(); // 레벨업
        }
        else // 없을 때
        {
            EquipWeapon(skillData.NewSkill(transform)); // 스킬 생성 및 장착
        }
    }

    private void Update()
    {
        // 매 프레임 각 무기의 “쿨타임 감소 + 필요 시 Attack()” 처리
        foreach (var w in attackSkillList)
            w.Tick();           // 내부에서 쿨타임 계산 후 Attack() 실행
    }
}
