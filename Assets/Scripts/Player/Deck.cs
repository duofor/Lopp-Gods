using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    
    public delegate void DeckUpdate(List<Card> e);
    public static event DeckUpdate deckWasUpdated;

    private SpriteRenderer spriteRenderer;
    public List<Card> deckCards;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
    }

    public void addCardToDeck(Card card) {//this also notifies the GUI
        this.deckCards.Add(card);
        deckWasUpdated(deckCards);
    }

    public List<Card> getDeck() {
        return deckCards;
    }
}

