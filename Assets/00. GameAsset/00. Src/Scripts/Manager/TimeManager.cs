using System;
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
    [SerializeField] private float bossWaveTime;
    private float lastBossWaveTime;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentTimeText;
    private int minutes;
    private int seconds;

    private Action bossWave = null;

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
            if (maxTime > currentTime && StageManager.instance.GetCurrentGameState() != InGameState.end)
            {
                currentTime += Time.deltaTime;
                minutes = (int)currentTime / 60;
                seconds = (int)currentTime % 60;
                currentTimeText.text = $"{minutes:00} : {seconds:00}";
                yield return null;
            }
            else if (StageManager.instance.GetCurrentGameState() == InGameState.end)
            {
                StageManager.instance.EndGame(currentTime);
                break;
            }
            else if (maxTime <= currentTime)
            {
                StageManager.instance.EndGame(currentTime);
                break;
            }

            int currentTimeInt = Mathf.FloorToInt(currentTime);
            if (currentTimeInt % Mathf.FloorToInt(bossWaveTime) == 0 && currentTimeInt != lastBossWaveTime)
            {
                ExecutionBossWave();
                lastBossWaveTime = currentTimeInt;
            }

        }
    }

    private void ExecutionBossWave()
    {
        if (bossWave != null)
        {
            bossWave();
            ResetBossWaveAction();
        }
    }

    public void AddBossWaveAction(Action function)
    {
        this.bossWave += function;
    }

    private void ResetBossWaveAction()
    {
        this.bossWave = null;
    }
}
