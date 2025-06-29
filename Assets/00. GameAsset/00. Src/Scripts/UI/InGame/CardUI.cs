// # Unity
using UnityEngine;
using UnityEngine.UI;

// # ETC
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Image               cardBackroundImage;
    [SerializeField]
    private Image               cardCooltimeImage;

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

    public async UniTaskVoid ProcessCooltime(float value)
    {
        float maxCooltime = value;
        float curCooltime = value;

        cardCooltimeImage.gameObject.SetActive(true);

        while(curCooltime > 0.0f)
        {
            curCooltime -= Time.deltaTime;
            curCooltime  = Mathf.Max(curCooltime, 0);

            cardCooltimeImage.fillAmount = curCooltime / maxCooltime;

            await UniTask.Yield(PlayerLoopTiming.Update);
        }
        cardCooltimeImage.gameObject.SetActive(false);
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
    public float GetUsedMoveDuration() { return usedMoveDuration; }
}
