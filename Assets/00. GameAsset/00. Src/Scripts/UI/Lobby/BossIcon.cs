using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;

public class BossIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public void OnPointerEnter(PointerEventData eventData)
	{
		transform.DOScale(Vector3.one * 1.25f, 0.1f);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.DOScale(Vector3.one, 0.1f);
	}
}
