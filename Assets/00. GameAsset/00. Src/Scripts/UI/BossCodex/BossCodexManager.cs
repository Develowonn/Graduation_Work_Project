using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossLogData
{
    public int bossID;
    public bool isDiscovered;
}

public class BossCodexManager : MonoBehaviour
{
    public static BossCodexManager Instance;

    private Dictionary<int, BossLogData> discoveredBosses = new Dictionary<int, BossLogData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DiscoverBoss(int bossID)
    {
        if (!discoveredBosses.ContainsKey(bossID))
        {
            discoveredBosses.Add(bossID, new BossLogData { bossID = bossID, isDiscovered = true });
        }
    }

    public bool IsDiscovered(int bossID)
    {
        return discoveredBosses.ContainsKey(bossID) && discoveredBosses[bossID].isDiscovered;
    }

    public List<int> GetAllDiscoveredBossIDs()
    {
        List<int> ids = new List<int>();
        foreach (var boss in discoveredBosses.Values)
        {
            if (boss.isDiscovered)
                ids.Add(boss.bossID);
        }
        return ids;
    }
}
