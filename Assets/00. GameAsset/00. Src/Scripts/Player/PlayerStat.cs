using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [Header("Level Stat")]
    [SerializeField] private int level;
    [SerializeField] private float maxExp;

    [Header("HP Stat")]
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    [Header("Movement Stat")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float minMovementSpeed;
    [SerializeField] private float maxMovementSpeed;

    [Header("Attack Power Stat")]
    [SerializeField] private float attackPower;
    [SerializeField] private float minAttackPower;
    [SerializeField] private float maxAttackPower;

    [Header("Damage Reduction Stat")]
    [SerializeField] private float damageReduction;

    [Header("UI")]
    [SerializeField] private Image expBar;
    [SerializeField] private Image hpBar;

    private float currentExp = 0;

    private void Start()
    {
        currentHP = maxHP;
    }

    public void GetExp(float exp)
    {
        currentExp += exp;
        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            level = level + 1;
            StageManager.instance.LevelUpPlayer();
        }
        expBar.fillAmount = currentExp / maxExp;
        Debug.Log($"ExpBar Value : {expBar.fillAmount} | currentExp / maxExp {currentExp / maxExp}");
    }

    public float GetMaxHP() { return maxHP; }
    public float GetCurrentHP() { return currentHP; }
    public float GetMovementSpeedStat() { return movementSpeed; }
    public float GetDamageReduction() { return damageReduction; }
    public float GetAttackPowerStat() { return attackPower; }

    public void IncreaseHP(float value)
    {
        currentHP = Mathf.Clamp(currentHP + value, 0, maxHP);
    }

    public void ReduceHP(float value)
    {
        currentHP = Mathf.Clamp(currentHP - value, 0, maxHP);
        hpBar.fillAmount = currentHP / maxHP;
        Debug.Log($"HpBar Value : {hpBar.fillAmount} | currentHp / maxHp {currentHP / maxHP}");
    }

    public void IncreaseMovementSpeed(float value)
    {
        movementSpeed = Mathf.Clamp(movementSpeed + value, minMovementSpeed, maxMovementSpeed);
    }

    public void ReduceMovementSpeed(float value)
    {
        movementSpeed = Mathf.Clamp(movementSpeed - value, minMovementSpeed, maxMovementSpeed);
    }

    public void  ModifyAttackPower(float value)
    {
        movementSpeed = Mathf.Clamp(attackPower + value, minAttackPower, maxAttackPower);
    }

    public void IncreaseDamageReduction(float value)
    {
        damageReduction = Mathf.Clamp01(damageReduction + value);
    }

    public void ReduceDamageReduction(float value)
    {
        damageReduction = Mathf.Clamp01(damageReduction - value);
    }
}
