// # Unity
using UnityEngine;
using UnityEngine.UI;

// # ETC
using TMPro;

public class CardUI : MonoBehaviour
{
    [SerializeField]
    private Image cardBackroundImage;

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

    [Space(10)]
    [SerializeField]
    private GameObject          cardIconObject;
    [SerializeField]
    private GameObject          cardNameObject;
    [SerializeField]
    private GameObject          cardInfoObject;
            
    private CardType            cardType;

    public void Initialize(string cardName, string cardInfo, Sprite iconSprite, CardType cardType)
    {
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
}
