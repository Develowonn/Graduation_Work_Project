using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillData", menuName = "Scriptable Objects/PlayerSkillData")]
public class PlayerSkillData : ScriptableObject
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
