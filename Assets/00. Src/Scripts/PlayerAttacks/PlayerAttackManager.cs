using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private readonly List<PlayerAttackSkill> attackSkillList = new();
    [SerializeField] private PlayerAttackSkill thunderStrikePrefab; // 임시

    void Start()
    {
        EquipWeapon(thunderStrikePrefab);  // ThunderStrike 오브젝트 인스턴스화됨
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
        // 매 프레임 각 무기의 “쿨타임 감소 + 필요 시 Attack()” 처리
        foreach (var w in attackSkillList)
            w.Tick();           // 내부에서 쿨타임 계산 후 Attack() 실행
    }
}
