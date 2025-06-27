using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private string playerName;

    public string GetPlayerName() { return playerName; }

    public void SetPlayerName(string value)
    {
        playerName = value;
    }
}