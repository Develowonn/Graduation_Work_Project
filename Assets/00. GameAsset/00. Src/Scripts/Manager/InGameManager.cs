using UnityEngine;

public class InGameManager : MonoBehaviour
{
	public static InGameManager Instance { get; private set; }

	[SerializeField]
	private VictoryPanel	victoryPanel;
	[SerializeField]
	private DefeatPanel		defeatPanel;

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
}
