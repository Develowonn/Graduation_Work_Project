// # System
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField]
    private int         maxDeckSize;
    [SerializeField]
    private Transform   deckParent;

    [Header("UI")]
    [SerializeField]
    private Transform   myDeckUIParents;
    private Animator    myDeckAnimator;
    private bool        isUsingDeck;

    private List<Card>  myDeck;
    private Card        selectedCard;

    private void Start()
    {
        myDeck = new List<Card>();

        myDeckAnimator = myDeckUIParents.GetComponent<Animator>();

        InitializeDeck();
    }

	private void Update()
	{
		//isUsingDeck = isUsingDeck != true;
		//myDeckAnimator.SetBool("IsUsingDeck", isUsingDeck);

		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            myDeck[0].Execute();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			myDeck[1].Execute();
		}
		else if(Input.GetKeyDown(KeyCode.Alpha3))
		{
			myDeck[2].Execute();
		}
	}

	private void InitializeDeck()
    {
        for(int i = 0; i < maxDeckSize; i++)
        {
            Card card = CardManager.Instance.GetRandomCard();
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