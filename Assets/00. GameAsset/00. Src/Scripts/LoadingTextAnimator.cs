// # System
using System.Collections;

// # Unity
using UnityEngine;

// # TMPro;
using TMPro;


public class LoadingTextAnimator : MonoBehaviour
{
	[Header("Loading Text Animator")]
	[SerializeField]
	private TMP_Text loadingText;
	[SerializeField]
	private string basedMessage;
	[SerializeField]
	private float interval;
	private int dotCount;

	private void Start()
	{
		StartCoroutine(AnimateLoadingText());
	}

	private IEnumerator AnimateLoadingText()
	{
		WaitForSeconds waitForSeconds = new WaitForSeconds(interval);

		while (true)
		{
			loadingText.text = basedMessage + new string('.', dotCount);

			dotCount = (dotCount + 1) % 4;

			yield return waitForSeconds;
		}
	}
}