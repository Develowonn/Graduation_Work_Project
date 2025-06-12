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

    [Header("Setting")]
    [SerializeField] private float resultTime;

    private void Awake()
    {
        instance = this;
    }

    public void GameEnd(float time)
    {
        resultTime = time;
        resultUI.SetActive(true);
        int minutes = (int)resultTime / 60;
        int seconds = (int)resultTime % 60;
        resultTimeText.text = $"LifeTime : {minutes:00}:{seconds:00}";
    }
}
