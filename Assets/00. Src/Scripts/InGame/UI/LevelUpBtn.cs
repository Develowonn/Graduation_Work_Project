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
        skillImage.sprite = playerSkillData.skillSprite;                                            // 스킬 이미지
        this.skillName.text = playerSkillData.skillName;                                            // 스킬 이름
        this.skillExplanation.text = playerSkillData.skillDesription;                               // 스킬 설명
        button.onClick.RemoveAllListeners();                                                        // 전에 남은 레벨업 제거
        button.onClick.AddListener(() => playerAttackManager.GetOrLevelUpSkill(playerSkillData));   // 지정된 스킬 레벨업
    }
}
