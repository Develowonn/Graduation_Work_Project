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
        skillImage.sprite = playerSkillData.skillSprite;                                            // ��ų �̹���
        this.skillName.text = playerSkillData.skillName;                                            // ��ų �̸�
        this.skillExplanation.text = playerSkillData.skillDesription;                               // ��ų ����
        button.onClick.RemoveAllListeners();                                                        // ���� ���� ������ ����
        button.onClick.AddListener(() => playerAttackManager.GetOrLevelUpSkill(playerSkillData));   // ������ ��ų ������
    }
}
