using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    
    public delegate void DeckUpdate(List<Card> e);
    public static event DeckUpdate deckWasUpdated;

    private SpriteRenderer spriteRenderer;
    
    public List<Card> deckCards;
    public List<Card> usedDeckCards;
    

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Card.cardUseEvent += removeCardFromDeck; 
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

    void removeCardFromDeck(Card card, RaycastHit2D hit) {
        deckCards.Remove(card);
        usedDeckCards.Add(card);
        //could move the card to a used deck
    }
}

