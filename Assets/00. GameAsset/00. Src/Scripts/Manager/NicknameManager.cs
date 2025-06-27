// # System
using System;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Etc
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class NicknameManager : MonoBehaviour
{
    [SerializeField]
    private _02_Lobby       lobby;
    
    [Space(10), SerializeField]
    private GameObject      errorMessagePanel;
    [SerializeField]
    private TextMeshProUGUI errorMessage;
    [SerializeField]
    private float           errorMessageDuration;

    [Header("Nickname Setting")]
    [SerializeField]
    private int             minInputfieldLength;
    [SerializeField]
    private int             maxInputfieldLength;

    [Header("Nickname Setting UI")]
    [SerializeField]
    private GameObject      nicknameSettingPanel;
    [SerializeField]
    private TMP_InputField  nicknameInputfield;
    [SerializeField]
    private Button          nicknameSettingButton;

    [Header("Error Message")]
    [SerializeField]
    private string          InputfieldLengthErrorMessage;

    private void Start()
    {
        nicknameSettingPanel.transform.localScale = Vector3.zero;
        errorMessagePanel.transform.localScale    = Vector3.zero;

        Utils.Dotween.PlayScaleAnimation(nicknameSettingPanel.transform, Vector3.one, 0.5f);

        Initialize();
    }

    private void Initialize()
    {
        nicknameSettingButton.onClick.AddListener(() => OnClickNicknameSettingButton(nicknameInputfield.text));
    }

    private void OnClickNicknameSettingButton(string text)
    {
        if (!IsInputfieldLengthInRange(text))
        {
            TriggerErrorMessage(InputfieldLengthErrorMessage).Forget();
            return;
        }

        Utils.Dotween.PlayScaleAnimation(nicknameSettingPanel.transform, Vector3.zero, 0.1f, 
            () => nicknameSettingPanel.SetActive(false));

        GameManager.Instance.SetPlayerName(text);
        lobby.SetProfileNicknameText(text);
    }

    private bool IsInputfieldLengthInRange(string text)
    {
        if (text.Length >= minInputfieldLength && text.Length <= maxInputfieldLength)
        {
            return true;
        }
        return false;
    }

    private async UniTaskVoid TriggerErrorMessage(string text)
    {
        errorMessagePanel.transform.DOScale(Vector3.one * 1.2f, 0.3f);
        errorMessage.text = text;

        await UniTask.Delay(TimeSpan.FromSeconds(errorMessageDuration));

        errorMessagePanel.transform.DOScale(Vector3.zero, 0.3f);
    }
}
