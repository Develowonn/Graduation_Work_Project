using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    private const float minPercent = 0.0f;
    private const float maxPercent = 1.0f;

    [Header("Level Stat")]
    [SerializeField] 
    private int         level;
    [SerializeField] 
    private float       maxExp;

    [Header("HP Stat")]
    [SerializeField] 
    private float       maxHP;
    [SerializeField] 
    private float       currentHP;

    [Header("Movement Stat")]
    [SerializeField]
    private float       movementSpeed;
    private float       maxMovementSpeed;

    [Header("Attack Power Stat")]
    [SerializeField] 
    private float       attackPower;
    private float       maxAttackPower;

    [Header("Damage Reduction Stat")]
    [SerializeField] 
    private float       damageReduction;

    [Header("UI")]
    [SerializeField] 
    private Image       expBar;
    [SerializeField] 
    private Image       hpBar;

    private float       currentExp = 0;

    private void Start()
    {
        currentHP        = maxHP;
        maxMovementSpeed = movementSpeed;
        maxAttackPower   = attackPower;
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

    public float GetMaxHP()             { return maxHP; }
    public float GetCurrentHP()         { return currentHP; }
    public float GetMovementSpeedStat() { return movementSpeed; }
    public float GetAttackPowerStat()   { return attackPower; }
    public float GetDamageReduction()   { return damageReduction; }

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

    public void SetMovementSpeed(float value)
    {
        movementSpeed = value;
    }

    public void ReduceMoveSpeedByPercent(float value)
    {
        float ratio     = Mathf.Clamp(value, minPercent, maxPercent);
        float reduction = movementSpeed * ratio;

        movementSpeed   = Mathf.Max(movementSpeed - reduction, 1);
    }

    public void SetAttackPower(float value)
    {
        attackPower = value;
    }

    public void ReduceAttackPowerByPercent(float value)
    {
        float ratio     = Mathf.Clamp(value, minPercent, maxPercent);
        float reduction = maxAttackPower * ratio;

        attackPower     = Mathf.Max(attackPower - reduction, 1);
    }
}