// # System
using System.Collections;

// # Unity
using UnityEngine;

// # Etc
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
	public static FadeManager Instance { get; private set; }

	[SerializeField]
	private RectTransform fadeImage;

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

	private IEnumerator Co_Fade()
	{
		FadeIn();
		yield return new WaitForSeconds(0.5f);
		FadeOut();
	}

	public void FadeIn()
	{
		fadeImage.sizeDelta = new Vector2(3000, 3000);
		fadeImage.DOSizeDelta(new Vector2(500, 500), 0.5f);
	}

	public void FadeOut()
	{
		fadeImage.sizeDelta = new Vector2(500, 500);
		fadeImage.DOSizeDelta(new Vector2(3000, 3000), 0.5f);
	}
}
