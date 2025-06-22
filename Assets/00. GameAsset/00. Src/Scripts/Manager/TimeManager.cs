using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [Header("TimeSetting")]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentTimeText;
    private int minutes;
    private int seconds;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(Co_TimeUpDate());
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    IEnumerator Co_TimeUpDate()
    {
        while (true)
        {
            if (maxTime > currentTime)
            {
                currentTime += Time.deltaTime;
                minutes = (int)currentTime / 60;
                seconds = (int)currentTime % 60;
                currentTimeText.text = $"{minutes:00} : {seconds:00}";
                yield return null;
            }
            else if (StageManager.instance.GetCurrentGameState() != InGameState.end)
            {
                currentTime = maxTime;
                StageManager.instance.EndGame(currentTime);
                break;
            }
        }
    }
}
