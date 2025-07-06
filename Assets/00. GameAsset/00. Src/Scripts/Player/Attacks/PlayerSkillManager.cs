using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    private readonly List<PlayerAttackSkill> attackSkillList = new();
    private Action<float, bool> OnCoolTimeBuff; // 버프를 한번에 실행할 액션 변수

    public void EquipSkill(PlayerAttackSkill skillPrefab)
    {
        attackSkillList.Add(skillPrefab);
        OnCoolTimeBuff += skillPrefab.OnCoolDownButff;
    }

    public void UnequipSkill(PlayerAttackSkill skill)
    {
        if (attackSkillList.Remove(skill))
        {
            OnCoolTimeBuff -= skill.OnCoolDownButff;
            Destroy(skill.gameObject);
        }
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
            EquipSkill(skillData.NewSkill(transform)); // 스킬 생성 및 장착
            skillData.LevelUpSkill(); // 레벨업
            skillData.inGameSkillObject.Init(GetComponent<PlayerStat>());
        }
    }

    /// <summary>
    /// 스킬 쿨타임 감소 버프 트리거 함수
    /// </summary>
    /// <param name="value">감소할 비율</param>
    /// <param name="isOn">활성화 여부</param>
    public void SkillCoolTimeBuffTrigger(float value, bool isOn) => OnCoolTimeBuff?.Invoke(value, isOn);

    private void Update()
    {
        // 매 프레임 각 무기의 “쿨타임 감소 + 필요 시 Attack()” 처리
        foreach (var w in attackSkillList)
            w.Tick();           // 내부에서 쿨타임 계산 후 Attack() 실행
    }
}
