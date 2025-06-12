using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] private int level;
    [SerializeField] private float maxExp;
    private float currentExp = 0;

    [Header("UI")]
    [SerializeField] private Slider expBar;

    public void GetExp(float exp)
    {
        currentExp += exp;
        if(currentExp >= maxExp)
        {
            currentExp -= maxExp;
            level = level + 1;
            StageManager.instance.PlayerLevelUp();
        }
        expBar.value = currentExp / maxExp;
    }
}
