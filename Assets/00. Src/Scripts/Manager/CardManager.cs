// # System
using System.Collections.Generic;

// # Unity
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [SerializeField]
    private List<CardSO> cardDatabase;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        cardDatabase = new List<CardSO>();
    }

    public Card GetRandomCard()
    {
        if (cardDatabase.Count <= 0) return null;

        int randomIndex = Random.Range(0, cardDatabase.Count);
        Card card       = Instantiate(cardDatabase[randomIndex].card);

        card.Initialize(cardDatabase[randomIndex].cardName, cardDatabase[randomIndex].cardType);
        return card;
    }
}
