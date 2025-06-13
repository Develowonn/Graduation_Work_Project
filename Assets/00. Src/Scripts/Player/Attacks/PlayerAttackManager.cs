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

        if (haveSkill) // ��ų�� �ִٸ�
        {
            skillData.LevelUpSkill(); // ������
        }
        else // ���� ��
        {
            EquipWeapon(skillData.NewSkill(transform)); // ��ų ���� �� ����
        }
    }

    private void Update()
    {
        // �� ������ �� ������ ����Ÿ�� ���� + �ʿ� �� Attack()�� ó��
        foreach (var w in attackSkillList)
            w.Tick();           // ���ο��� ��Ÿ�� ��� �� Attack() ����
    }
}
