using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpBtn : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image skillImage;
    [SerializeField] private TextMeshProUGUI skillName;
    [SerializeField] private TextMeshProUGUI skillExplanation;

    public void InitBtn(Sprite sprite, string skillName, string skillExplanation)
    {
        skillImage.sprite = sprite;
        this.skillName.text = skillName;
        this.skillExplanation.text = skillExplanation;
    }
}
