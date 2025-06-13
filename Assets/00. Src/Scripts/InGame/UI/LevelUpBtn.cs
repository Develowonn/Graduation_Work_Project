using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBtn : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillExplanation;
    [SerializeField] private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    public void InitBtn(PlayerSkillData playerSkillData, PlayerAttackManager playerAttackManager)
    {
        skillImage.sprite = playerSkillData.skillSprite;
        this.skillName.text = playerSkillData.skillName;
        this.skillExplanation.text = playerSkillData.skillDesription;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => playerAttackManager.GetOrLevelUpSkill(playerSkillData));
    }
}
