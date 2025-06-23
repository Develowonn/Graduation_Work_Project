// # System
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private const int   maxDeckSize = 3;

    [SerializeField]
    private Transform   deckParent;

    [Header("UI")]
    [SerializeField]
    private CardUI[]    cardUiList;

    private List<Card>  myDeck;
    private Card        selectedCard;

    private void Start()
    {
        myDeck = new List<Card>();

        InitializeDeck();
    }

	private void InitializeDeck()
    {
        for(int i = 0; i < maxDeckSize; i++)
        {
            CardSO  cardSO  = CardManager.Instance.GetRandomCardSO();
            cardUiList[i].Initialize(cardSO.cardName, cardSO.cardInfo, cardSO.cardSprite, cardSO.cardType);
            cardUiList[i].ActivateBack();

            Card    card    = Instantiate(cardSO.card);
            card.transform.SetParent(deckParent);
			myDeck.Add(card);
        }
    }

    public void UseCard()
    {
        selectedCard.Execute();
    }

    public void SelectCard(string cardName)
    {
        Card card = myDeck.Find(x => x.GetCardName() == cardName);

        if (card != null) selectedCard = card;
        else
        {
            Debug.LogWarning($"DeckManager_{cardName} 카드를 찾을 수 없습니다");
        }
    }

    public void RemoveCard(string cardName)
    {
        Card card = myDeck.Find(x => x.GetCardName() == cardName);

        if (card != null) myDeck.Remove(card);
        else
        {
            Debug.LogWarning($"DeckManager_{cardName} 카드를 찾을 수 없습니다");
        }
    }

    public void RemoveCard(Card card)
    {
        Card _card = myDeck.Find(x => x == card);

        if (card != null) myDeck.Remove(_card);
        else
        {
            Debug.LogWarning($"DeckManager_{card.GetCardName()} 카드를 찾을 수 없습니다");
        }
    }
}