using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class InGameManager : MonoBehaviour
{
	public static InGameManager Instance { get; private set; }

	[SerializeField]
	private VictoryPanel	victoryPanel;
	[SerializeField]
	private DefeatPanel		defeatPanel;

	[SerializeField]
	private Image		    bossLogoImage;

	[SerializeField]
	private GameObject		playerObject;

	private int				caughtMonsterCount;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		} 
		else Destroy(gameObject);
	}

	public GameObject   GetPlayerObject()		{ return playerObject; }
	public int		    GetCaughtMonsterCount() {  return caughtMonsterCount; }
	public VictoryPanel GetVictoryPanel()		{  return victoryPanel; }
	public DefeatPanel  GetDefeatPanel()		{  return defeatPanel; }

	public void IncreaseCaughtMonsterCount() { caughtMonsterCount++; }

	public void SetBossLogoActivity(bool activity)
	{
		switch (activity) 
		{
			case true:
				bossLogoImage.DOFade(1.0f, 1.0f);
				break;
			case false:
				bossLogoImage.DOFade(0.0f, 1.0f);
				break;
		}
	}
}
