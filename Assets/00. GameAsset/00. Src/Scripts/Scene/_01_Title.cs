// # System
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.SceneManagement;

// # TMPro;
using TMPro;

public class _01_Title : MonoBehaviour
{
	[SerializeField]
	private string		loadingSceneName;
	[SerializeField]
	private float		minLoadingTime;

	private void Start()
	{
		StartCoroutine(LoadSceneAsync(loadingSceneName));
	}

	private IEnumerator LoadSceneAsync(string sceneName)
	{
        float startTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncLoad.allowSceneActivation = false;

        while (Time.time - startTime < minLoadingTime || !asyncLoad.isDone)
        {
            if (asyncLoad.progress >= 0.9f && Time.time - startTime >= minLoadingTime)
            {
                asyncLoad.allowSceneActivation = true;
            }
            yield return null;
        }
	}
}