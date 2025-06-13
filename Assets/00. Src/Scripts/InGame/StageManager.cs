using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

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
    [SerializeField] private PlayerAttackManager playerAttackManager;
    private int playerLevelUpCount = 0;
    [SerializeField] private List<PlayerSkillData> playerSkillDataList = new List<PlayerSkillData>();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        LevelUpPlayer();
    }

    public void LevelUpPlayer()
    {
        if (levelUpPanel.activeSelf == false)
        {
            Time.timeScale = 0f;
            levelUpPanel.SetActive(true);
            InitLevelUpBtn(playerSkillDataList[Random.Range(0, playerSkillDataList.Count)], levelUpBtn_1);
            InitLevelUpBtn(playerSkillDataList[Random.Range(0, playerSkillDataList.Count)], levelUpBtn_2);
            InitLevelUpBtn(playerSkillDataList[Random.Range(0, playerSkillDataList.Count)], levelUpBtn_3);
        }
        else playerLevelUpCount++;
    }

    public void InitLevelUpBtn(PlayerSkillData skillData, LevelUpBtn btn)
    {
        Debug.Log("추가 : " + skillData.skillName);
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
        resultTime = time;
        resultUI.SetActive(true);
        int minutes = (int)resultTime / 60;
        int seconds = (int)resultTime % 60;
        resultTimeText.text = $"LifeTime : {minutes:00}:{seconds:00}";
    }
}
