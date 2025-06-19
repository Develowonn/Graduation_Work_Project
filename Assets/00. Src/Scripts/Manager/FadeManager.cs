// # System
using System;
using System.Collections;

// # Unity
using UnityEngine;

// # Etc
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
	public static FadeManager Instance { get; private set; }

	[SerializeField]
	private Vector2		  fadeInTargetSize  = new Vector2(0, 0);
	[SerializeField]
	private Vector2		  fadeOutTargetSize = new Vector2(3000, 3000);

	[SerializeField]
	private RectTransform fadeImage;
	[SerializeField]
	private float		  fadeDuration;

	private void Awake()
	{
		if(Instance == null)
			Instance = this;
		else
			Destroy(gameObject);
	}

	public void Fade()
	{
		StartCoroutine(Co_Fade());
	}

	public void Fade(TweenCallback tweenCallback)
	{
		StartCoroutine(Co_Fade(tweenCallback));
	}

	private IEnumerator Co_Fade()
	{
		yield return new WaitForSeconds(0.3f);
		fadeImage.gameObject.SetActive(true);

		FadeIn();
		yield return new WaitForSeconds(fadeDuration);
		FadeOut(() => fadeImage.gameObject.SetActive(false));
	}

	private IEnumerator Co_Fade(TweenCallback tweenCallback)
	{
		yield return new WaitForSeconds(0.3f);
		fadeImage.gameObject.SetActive(true);

		FadeIn();
		yield return new WaitForSeconds(fadeDuration);
		FadeOut(() => fadeImage.gameObject.SetActive(false), tweenCallback);
	}

	public void FadeIn()
	{
		fadeImage.sizeDelta = fadeOutTargetSize;
		fadeImage.DOSizeDelta(fadeInTargetSize, fadeDuration);
	}

	public void FadeOut()
	{
		fadeImage.sizeDelta = fadeInTargetSize;
		fadeImage.DOSizeDelta(fadeOutTargetSize, fadeDuration);
	}

	public void FadeOut(TweenCallback tweenCallback)
	{
		fadeImage.sizeDelta = fadeInTargetSize;
		fadeImage.DOSizeDelta(fadeOutTargetSize, fadeDuration).OnComplete(tweenCallback);
	}

	public void FadeOut(TweenCallback tweenCallback, TweenCallback tweenCallback1)
	{
		fadeImage.sizeDelta = fadeInTargetSize;
		fadeImage.DOSizeDelta(fadeOutTargetSize, fadeDuration).OnComplete(tweenCallback).OnComplete(tweenCallback1);
	}
}
