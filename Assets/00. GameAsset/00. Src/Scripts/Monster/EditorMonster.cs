using UnityEngine;

public class EditorMonster : MonoBehaviour
{
	[SerializeField]
	private string	monsterName;

	public string GetMonsterName() {  return monsterName; }
}
