using UnityEngine;

public class KeyManager : Singleton<KeyManager>
{
	[SerializeField]
	private KeyCode firstCardKey;
	[SerializeField]
	private KeyCode secondCardKey;
	[SerializeField] 
	private KeyCode thirdCardKey;

	public KeyCode GetFirstCardKey()	{ return firstCardKey; }
	public KeyCode GetSecondCardKey()	{ return secondCardKey; }
	public KeyCode GetThirdCardKey()	{ return thirdCardKey; }
}
