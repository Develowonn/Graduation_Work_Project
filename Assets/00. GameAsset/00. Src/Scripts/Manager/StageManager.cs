using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public enum InGameState
{
    playing,
    end
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("State")]
    [SerializeField] private InGameState currentGameState;

    [Header("UI")]
    [SerializeField] private GameObject resultUI;
    [SerializeField] private TextMeshProUGUI resultTimeText;
    [SerializeField] private Button returnButton;
    [Space(10f)]
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private LevelUpBtn levelUpBtn_1;
    [SerializeField] private LevelUpBtn levelUpBtn_2;
    [SerializeField] private LevelUpBtn levelUpBtn_3;

    [Header("Setting")]
    [SerializeField] private float resultTime;

    [Header("Player")]
    [SerializeField] private PlayerStat playerStat;
    [SerializeField] private PlayerSkillManager playerAttackManager;
    private int playerLevelUpCount = 0;
    [SerializeField] private List<SkillSO> playerSkillDataList = new List<SkillSO>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        currentGameState = InGameState.playing;
    }

    public InGameState GetCurrentGameState()
    {
        return currentGameState;
    }

    public void LevelUpPlayer()
    {
        if (levelUpPanel.activeSelf == false)
        {
            Time.timeScale = 0f;
            levelUpPanel.SetActive(true);
            InitLevelUpBtn(GetRandomSkill(), levelUpBtn_1);
            InitLevelUpBtn(GetRandomSkill(), levelUpBtn_2);
            InitLevelUpBtn(GetRandomSkill(), levelUpBtn_3);
        }
        else playerLevelUpCount++;
    }

    public SkillSO GetRandomSkill()
    {
        List<SkillSO> availableSkills = playerSkillDataList.FindAll(
        s => s.inGameSkillObject == null || s.inGameSkillObject.GetSkillLevel() < 5);

        if (availableSkills.Count == 0)
        {
            Debug.LogWarning("모든 스킬이 최대 레벨입니다!");
            return null; // TODO. 모든 스킬 최대 레벨 시 시스템 구현
        }

        int index = Random.Range(0, availableSkills.Count);
        return availableSkills[index];
    }

    public void InitLevelUpBtn(SkillSO skillData, LevelUpBtn btn)
    {
        btn.InitBtn(skillData, playerAttackManager); // 버튼 초기화 (강화할 스킬, 플레이어 공격 매니저)
        PopAnimate(btn.transform);
        btn.GetComponent<Button>().onClick.AddListener(ClosePlayerLevelUpPanel); // 선택시 패널 끄기 추가
    }

    private void ClosePlayerLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        if (playerLevelUpCount > 0)
        {
            LevelUpPlayer();
            playerLevelUpCount--;
        }
    }

    private void PopAnimate(Transform target)
    {
        target.localScale = Vector3.zero;
        target.DOScale(Vector3.one, 0.3f)
              .SetEase(Ease.OutBack)
              .SetUpdate(true);
    }

    public void DieMonster(float exp)
    {
        playerStat.GetExp(exp);
    }

    public void EndGame(float time)
    {
        currentGameState = InGameState.end;
        resultTime = time;
        resultUI.SetActive(true);
        int minutes = (int)resultTime / 60;
        int seconds = (int)resultTime % 60;
        resultTimeText.text = $"LifeTime : {minutes:00}:{seconds:00}";
    }
}
