// # System
using System;

// # Unity
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// # Etc
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class _00_Logo : MonoBehaviour
{
	[SerializeField]
	private Image		logoImage;
	[SerializeField]
	private float		duration;

	private void Start()
	{
		FadeLogo().Forget();
	}

	private async UniTaskVoid FadeLogo()
	{
		bool isDone = false;

		logoImage.DOFade(1.0f, duration).SetEase(Ease.Linear).OnComplete(() =>
		{
			isDone = true;
		});

		await UniTask.WaitUntil(() => isDone);
		await UniTask.Delay(TimeSpan.FromSeconds(1.0f));

		SceneManager.LoadScene("01. Title");
	}
}
