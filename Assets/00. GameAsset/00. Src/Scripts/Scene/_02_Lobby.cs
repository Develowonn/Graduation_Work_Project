// # System
using System;
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # ETC
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using System.Threading;

public class _02_Lobby : MonoBehaviour
{
	public static _02_Lobby Instance { get; private set; }

	[Header("Dungeon UI")]
	[SerializeField]
	private Button dungeonOpenButton;
	[SerializeField]
	private Button dungeonStartButton;
	[SerializeField]
	private GameObject dungeonPanel;
	[SerializeField]
	private string dungeonName;

	[Header("Error UI")]
	[SerializeField]
	private Image errorUiImage;
	[SerializeField]
	private TMP_Text errorUiText;
	[SerializeField]
	private float showTime;
	[SerializeField]
	private float fadeDuration;

	[Header("Text UI")]
	[SerializeField]
	private TMP_Text profileNicknameText;
	[SerializeField]
	private TMP_Text playerGoldText;
	[SerializeField]
	private TMP_Text playerEnergyText;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		InitializeDungeonUI();

		UpdatePlayerGoldText();
		UpdatePlayerEnergyText();

		FadeManager.Instance.Fade();
	}

	public void InitializeDungeonUI()
	{
		dungeonOpenButton.onClick.AddListener(() => OnClickDungeonOpenButton());
		dungeonStartButton.onClick.AddListener(() => StartCoroutine(OnClickDungeonStartButtonCoroutine()));
		dungeonPanel.SetActive(false);
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

		if (gameManager.IsEnergtInfinity())
		{
			playerEnergyText.text = $"¡Ä / ¡Ä";
		}
		else
		{
			playerEnergyText.text = $"{gameManager.GetCurrentEnergy()}/{gameManager.GetMaxEnergy()}";
		}
	}

	private void OnClickDungeonOpenButton()
	{
		dungeonPanel.gameObject.SetActive(true);
		Utils.Dotween.PlayScaleAnimation(dungeonPanel.transform, Vector3.one, 0.4f);
	}

	private IEnumerator OnClickDungeonStartButtonCoroutine()
	{
		float startTime = Time.time;

		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(dungeonName);
		asyncLoad.allowSceneActivation = false;

		while (!asyncLoad.isDone)
		{
			if (asyncLoad.progress >= 0.9f)
			{
				asyncLoad.allowSceneActivation = true;
			}
			yield return null;
		}
	}
}
