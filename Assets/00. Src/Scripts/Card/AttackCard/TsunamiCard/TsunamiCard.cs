using UnityEngine;

public class TsunamiCard : Card
{
	[SerializeField]
	private int				tornadoCount;

	[Header("VFX")]
	[SerializeField]
	private GameObject		auraVfx;
	[SerializeField]
	private GameObject      paritcleVfx;
	[SerializeField]
	private GameObject		tornadoVfx;

	public override void Execute()
	{
	}
}