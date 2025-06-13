using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("TimeSetting")]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI currentTimeText;
    private int minutes;
    private int seconds;

    private void Start()
    {
        StartCoroutine(Co_TimeUpDate());
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
            else
            {
                currentTime = maxTime;
                StageManager.instance.EndGame(currentTime);
                break;
            }
        }
    }
}
