using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BossCodex
{
    public BossData bossData;
    public GameObject bossObject;
    public Image bossIcon;
    public Button selectButton;
}

public class BossCodexUIManager : MonoBehaviour
{
    [Header("Codex")]
    [SerializeField] private List<BossCodex> bossCodex;
    [SerializeField] private RawImage renderImage;
    [SerializeField] private TextMeshProUGUI bossNameText;
    [SerializeField] private TextMeshProUGUI bossDescriptionText;

    [Header("OtherUI")]
    [SerializeField] private GameObject codexPanelBackGround;
    [SerializeField] private Button codexButton;
    [SerializeField] private Button offButton;

    private void Start()
    {
        codexButton.onClick.AddListener(() => SetActive(true));
        offButton.onClick.AddListener(() => SetActive(false));
        Init();
    }

    private void Init()
    {
        foreach (var boss in bossCodex)
        {
            boss.selectButton.onClick.AddListener(() => { SelectBoss(boss); });
            if (BossCodexManager.Instance.IsDiscovered(boss.bossData.bossID)) boss.bossIcon.color = Color.white;
        }
    }

    private void SetActive(bool isActive)
    {
        codexPanelBackGround.SetActive(isActive);
    }

    private void SelectBoss(BossCodex bossCodex)
    {
        if (BossCodexManager.Instance.IsDiscovered(bossCodex.bossData.bossID))
        {
            renderImage.color = Color.white;
            bossNameText.text = bossCodex.bossData.bossName;
            bossDescriptionText.text = bossCodex.bossData.description;
        }
        else
        {
            renderImage.color = Color.black;
            bossNameText.text = "???";
            bossDescriptionText.text = "아직 발견하지 못했습니다! 모험을 떠나세요!";
        }

        bossCodex.bossObject.SetActive(true);
        foreach (var boss in this.bossCodex)
        {
            if (boss != bossCodex) boss.bossObject.SetActive(false);
        }
    }
}
