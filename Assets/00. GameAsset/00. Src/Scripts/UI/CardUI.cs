// # Unity
using UnityEngine;
using UnityEngine.UI;

// # ETC
using DG.Tweening;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Image               cardBackroundImage;

    [Header("Back")]
    [SerializeField]
    private Sprite              cardBuffBackSprite;
    [SerializeField]
    private Sprite              cardAttackBackSprite;

    [Header("Front")]
    [SerializeField]
    private Sprite              cardFrontSprite;
    [SerializeField]
    private Image               cardIconImage;
    [SerializeField]
    private TextMeshProUGUI     cardNameText;
    [SerializeField]
    private TextMeshProUGUI     cardInfoText;

    [Header("Selected Effect")]
    [SerializeField]
    private Vector3             selectedScale;
	[SerializeField]    
    private float               scaleDuration;

	[Header("Used Effect")]
	[SerializeField]
	private Vector2             usedPositionOffset;
	[SerializeField]
	private float               usedMoveDuration;
	[SerializeField]
	private Vector3             usedScale;

	private CardType            cardType;
    private RectTransform       rectTransform;

    public float GetUsedMoveDuration() { return usedMoveDuration; }
        
    public void Initialize(string cardName, string cardInfo, Sprite iconSprite, CardType cardType)
    {
        rectTransform = GetComponent<RectTransform>();

        cardIconImage.sprite = iconSprite;
        cardNameText.text    = cardName;
        cardInfoText.text    = cardInfo;

        this.cardType        = cardType;
    } 

    public void ActivateBack()
    {
        cardBackroundImage.sprite = cardType == CardType.Attack ? cardAttackBackSprite : cardBuffBackSprite;

        cardIconImage.gameObject.SetActive(false);
        cardNameText.gameObject.SetActive(false);
        cardInfoText.gameObject.SetActive(false);
    }

    public void ActivateFront()
    {
        cardBackroundImage.sprite = cardFrontSprite;

        cardIconImage.gameObject.SetActive(true);
        cardNameText.gameObject.SetActive(true);
        cardInfoText.gameObject.SetActive(true);
    }

    public void OnUsed()
    {
        rectTransform.DOScale(usedScale, scaleDuration).OnComplete(() =>
        {
			rectTransform.DOAnchorPos(rectTransform.anchoredPosition + usedPositionOffset, usedMoveDuration);
		});
    }

    public void OnSelected()
    {
        rectTransform.DOScale(selectedScale, scaleDuration);
    }

    public void OnUnSelected()
    {
		rectTransform.DOScale(Vector3.one, scaleDuration);

	}
}
