using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private readonly List<PlayerAttackSkill> attackSkillList = new();
    [SerializeField] private PlayerAttackSkill thunderStrikePrefab; // �ӽ�

    void Start()
    {
        EquipWeapon(thunderStrikePrefab);  // ThunderStrike ������Ʈ �ν��Ͻ�ȭ��
    }


    public void EquipWeapon(PlayerAttackSkill weaponPrefab)
    {
        var w = Instantiate(weaponPrefab, transform);
        attackSkillList.Add(w);
    }

    public void UnequipWeapon(PlayerAttackSkill weapon)
    {
        if (attackSkillList.Remove(weapon))
            Destroy(weapon.gameObject);
    }

    private void Update()
    {
        // �� ������ �� ������ ����Ÿ�� ���� + �ʿ� �� Attack()�� ó��
        foreach (var w in attackSkillList)
            w.Tick();           // ���ο��� ��Ÿ�� ��� �� Attack() ����
    }
}
