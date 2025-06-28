// # System
using System;

// # Unity
using UnityEngine;
using UnityEngine.UI;

// # Etc
using Cysharp.Threading.Tasks;
using DG.Tweening;

public abstract class UiButton : MonoBehaviour
{
	[SerializeField]
	private bool	isUsing;

	private Button  uiButton;
	private Image   uiImage;

	private void Start()
	{
		uiButton = GetComponent<Button>();
		uiImage  = GetComponent<Image>();

		ColorBlock colorBlock = uiButton.colors;

		switch (isUsing)
		{
			case true:
				colorBlock.normalColor		= new Color(1, 1, 1, 0.2f);
				colorBlock.highlightedColor = new Color32(255, 119, 0, 180);
				colorBlock.selectedColor	= new Color(1, 1, 1, 0.2f);
				colorBlock.pressedColor		= new Color(1, 1, 1, 0.2f);

				uiButton.colors = colorBlock;
				uiButton.onClick.AddListener(() => OnClick()); 
				break;

			case false: 
				colorBlock.normalColor		= new Color32(46, 46, 46, 255);
				colorBlock.pressedColor		= new Color32(46, 46, 46, 255);
				colorBlock.selectedColor    = new Color32(46, 46, 46, 255);
				colorBlock.highlightedColor = new Color(0, 0, 0); 

				uiButton.colors = colorBlock;
				uiImage.DOFade(0.6f, 0.0f);

				uiButton.onClick.AddListener(() => _02_Lobby.Instance.TriggerErrorMessage()); 
				break;
		}
	}

	public abstract void OnClick();
}
