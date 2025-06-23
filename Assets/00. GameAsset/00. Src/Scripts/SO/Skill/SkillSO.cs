using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string skillName;                                // 스킬 이름
    public Sprite skillSprite;                              // 스킬 아이콘
    public string skillDesription;                          // 스킬 설명
    public PlayerAttackSkill skillObject;                   // 스킬 사용 프리팹
    public PlayerAttackSkill inGameSkillObject;             // 게임 플레이 시 참조용 인게임 오브젝트
    public GradeSO gradeSO;                                 // 등급 SO

    public void LevelUpSkill()
    {
        inGameSkillObject.UPLevel();
    }

    public PlayerAttackSkill NewSkill(Transform t)
    {
        inGameSkillObject = Instantiate(skillObject, t);
        return inGameSkillObject;
    }
}
