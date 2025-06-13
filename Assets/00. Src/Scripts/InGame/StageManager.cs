using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    private int playerLevelUpCount = 0;

    private void Awake()
    {
        instance = this;
        levelUpBtn_1.GetComponent<Button>().onClick.AddListener(ClosePlayerLevelUpPanel);
        levelUpBtn_2.GetComponent<Button>().onClick.AddListener(ClosePlayerLevelUpPanel);
        levelUpBtn_3.GetComponent<Button>().onClick.AddListener(ClosePlayerLevelUpPanel);
    }

    public void LevelUpPlayer()
    {
        if (levelUpPanel.activeSelf == false)
        {
            Time.timeScale = 0f;
            levelUpPanel.SetActive(true);
            PopAnimate(levelUpBtn_1.transform);
            PopAnimate(levelUpBtn_2.transform);
            PopAnimate(levelUpBtn_3.transform);
        }
        else playerLevelUpCount++;
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
