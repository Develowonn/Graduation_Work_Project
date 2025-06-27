// # System
using System;

// # Unity
using UnityEngine;

// # ETC
using TMPro;

public class _02_Lobby : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TMP_Text profileNicknameText;
    [SerializeField]
    private TMP_Text playerGoldText;
    [SerializeField]
    private TMP_Text playerEnergyText;

    private void Start()
    {
        UpdatePlayerGoldText();
        UpdatePlayerEnergyText();

        FadeManager.Instance.Fade();
    }

    public void SetProfileNicknameText(string text)
    {
        profileNicknameText.text = text;
    }

    public void UpdatePlayerGoldText()
    {
        playerGoldText.text = GameManager.Instance.GetPlayerGold().ToString();
    }

    public void UpdatePlayerEnergyText()
    {
        GameManager gameManager = GameManager.Instance;
        playerEnergyText.text   = $"{gameManager.GetCurrentEnergy().ToString()}/{gameManager.GetMaxEnergy().ToString()}";
    }
}
