using UnityEngine;

public class InGameManager : MonoBehaviour
{
	public static InGameManager Instance { get; private set; }

	[SerializeField]
	private GameObject	playerObject;

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		} 
		else Destroy(gameObject);
	}

	public GameObject GetPlayerObject() { return playerObject; }
}
