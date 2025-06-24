using UnityEngine;

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

	public bool IsIntro() { return isIntro; }
}
