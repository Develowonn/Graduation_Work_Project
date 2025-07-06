// # System
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Etc
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
	[SerializeField]
	private TMP_Text	goldText;
	[SerializeField]
	private TMP_Text	caughtMonsterCountText;
	[SerializeField]
	private TMP_Text	timeText;

	[SerializeField]
	private Button		lobbyButton;

	[SerializeField]
	private GameObject	backgroundPanel;

	private void Awake()
	{
		lobbyButton.onClick.AddListener(() =>
		{
			StartCoroutine(LoadSceneCoroutine("02. Lobby"));
		});

		backgroundPanel.gameObject.SetActive(false);
	}

	public void SetActive(int gold, int caughtMonsterCount, string time)
	{
		backgroundPanel.SetActive(true);

		goldText.text = gold.ToString() + " gold";
		caughtMonsterCountText.text = caughtMonsterCount.ToString() + " ¸¶¸®"; ;
		timeText.text = time;

		transform.DOScale(Vector3.one, 0.5f);
	}

	public IEnumerator LoadSceneCoroutine(string sceneName)
	{
		AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
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
