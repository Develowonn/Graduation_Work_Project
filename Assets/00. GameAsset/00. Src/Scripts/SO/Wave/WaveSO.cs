using UnityEngine;

[CreateAssetMenu(fileName = "WaveSO", menuName = "Scriptable Objects/WaveSO")]
public class WaveSO : ScriptableObject
{
	public GameObject	monsters;

	[Header("Time")]
	public double		startTime;
	public float		spawnTime;
}