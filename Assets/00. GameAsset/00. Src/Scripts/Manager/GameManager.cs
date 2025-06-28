using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int     maxEnergy;

    private string  playerName;
    private int     playerGold;
    private int     currentEnergy;

    public string GetPlayerName()    { return playerName; }
    public int    GetPlayerGold()    { return playerGold; }
    public int    GetMaxEnergy()     { return maxEnergy; }
    public int    GetCurrentEnergy() { return currentEnergy; }

	private void Start()
	{
		currentEnergy = maxEnergy;
	}

	public void SetPlayerName(string value)
    {
        playerName = value;
    }

    public void AddPlayerGold(int gold)
    {
        playerGold += gold;
    }
}