using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    private readonly List<PlayerAttackWeapon> weapons = new();
    [SerializeField] private PlayerAttackWeapon thunderStrikePrefab;

    void Start()
    {
        EquipWeapon(thunderStrikePrefab);  // ThunderStrike ������Ʈ �ν��Ͻ�ȭ��
    }


    public void EquipWeapon(PlayerAttackWeapon weaponPrefab)
    {
        var w = Instantiate(weaponPrefab, transform);
        weapons.Add(w);
    }

    public void UnequipWeapon(PlayerAttackWeapon weapon)
    {
        if (weapons.Remove(weapon))
            Destroy(weapon.gameObject);
    }

    private void Update()
    {
        // �� ������ �� ������ ����Ÿ�� ���� + �ʿ� �� Attack()�� ó��
        foreach (var w in weapons)
            w.Tick();           // ���ο��� ��Ÿ�� ��� �� Attack() ����
    }
}
