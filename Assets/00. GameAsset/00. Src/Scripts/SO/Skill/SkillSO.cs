using UnityEngine;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Scriptable Objects/SkillSO")]
public class SkillSO : ScriptableObject
{
    public string skillName;
    public Sprite skillSprite;
    public string skillDesription;
    public PlayerAttackSkill skillObject;
    public PlayerAttackSkill inGameSkillObject;

    public void LevelUpSkill()
    {
        inGameSkillObject.LevelUP();
    }

    public PlayerAttackSkill NewSkill(Transform t)
    {
        inGameSkillObject = Instantiate(skillObject, t);
        return inGameSkillObject;
    }
}
