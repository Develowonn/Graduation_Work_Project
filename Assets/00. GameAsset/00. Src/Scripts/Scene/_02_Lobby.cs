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

    private void Start()
    {
        FadeManager.Instance.Fade();
    }

    public void SetProfileNickname(string text)
    {
        profileNicknameText.text = text;
    }
}
