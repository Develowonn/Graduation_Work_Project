using UnityEngine;

public class _03_Game : MonoBehaviour
{
	private void Start()
	{
		FadeManager.Instance.Fade(() => StageManager.instance.LevelUpPlayer());
	}
}
