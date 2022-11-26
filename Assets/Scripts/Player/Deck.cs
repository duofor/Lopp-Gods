using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    
    private SpriteRenderer spriteRenderer;
    public List<Card> deckCards;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
    }

    public void addCardToDeck(Card card) {
        this.deckCards.Add(card);
    }

    public List<Card> getDeck() {
        return deckCards;
    }
}

