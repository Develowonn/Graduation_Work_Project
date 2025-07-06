// # System
using System.Collections;

// # Unity
using UnityEngine;
using UnityEngine.SceneManagement;

public class _03_Game : MonoBehaviour
{
	public static _03_Game Instance { get; private set; }

	[SerializeField]
	private Animator myDeckUIAnimator;

	private int		 introTriggerHash;

	private bool	 isIntro;
	private bool	 isAnimating;

    private void Awake()
    {
		if (Instance == null)
			Instance = this;
    }

    private void Start()
	{
		introTriggerHash = Animator.StringToHash("OnIntro");

		FadeManager.Instance.Fade(() => TriggerMyDeckIntro());
	}

    private void Update()
    {
        if (isAnimating)
        {
			CheckAnimateState();
        }

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			StartCoroutine(LoadSceneCoroutine("02. Lobby"));
		}
    }

    private void TriggerMyDeckIntro()
    {
		myDeckUIAnimator.gameObject.SetActive(true);
		myDeckUIAnimator.SetTrigger(introTriggerHash);

		isAnimating = true;
    }

	private void CheckAnimateState()
    {
		if(myDeckUIAnimator != null)
        {
			AnimatorStateInfo stateInfo = myDeckUIAnimator.GetCurrentAnimatorStateInfo(0);
			if(stateInfo.IsName("Intro") && stateInfo.normalizedTime >= 1.0f && !myDeckUIAnimator.IsInTransition(0))
            {
				myDeckUIAnimator.enabled = false;
				isAnimating	= false;
				isIntro		= true;

				StageManager.instance.LevelUpPlayer();
            }
        }
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

	public bool IsIntro() { return isIntro; }
}
