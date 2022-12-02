using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {
    
    public delegate void DeckUpdate(List<Card> e);
    public static event DeckUpdate cardHandUpdateEvent;

    private SpriteRenderer spriteRenderer;
    
    public List<Card> deckCards;
    public List<Card> usedDeckCards;
    public List<Card> cardsInHand;

    public List<Card> availableCards;

    void Awake() {
        initDeck(); //this will cause problems.
        spriteRenderer = GetComponent<SpriteRenderer>();
        Card.cardUseEvent += removeCardFromHand; 
    }

    void Update() {
        if (deckCards.Count == 0 && usedDeckCards.Count > 0 && cardsInHand.Count == 0 ) {
            moveCardsFromUsedDeckToPrimaryDeck();
        }
    }

    void OnMouseOver() {
        if ( deckCards.Count == 0 )
            return;

        if (Input.GetMouseButtonDown(0)) { //this is for testing only.
            drawCardFromDeck();
        }
    }

    public void addCardToDeck(Card card) {//this also notifies the GUI
        List<Card> temp = new List<Card>();

        foreach(Card tempCard in deckCards) {
            temp.Add(tempCard);
        }
        temp.Add(card);

        deckCards = temp;
    }

    public List<Card> getDeck() {
        return deckCards;
    }

    void removeCardFromHand(Card card, RaycastHit2D hit, int damage) {
        cardsInHand.Remove(card);
        usedDeckCards.Add(card);

        cardHandUpdateEvent(cardsInHand); //show in UI
    }

    public void drawCardFromDeck() { //returns cardsInHand
        if ( deckCards.Count == 0 ) {
            //we cannot draw card since deck is empty
            return;
        }

        int randomDeckIndex = Random.Range(0, deckCards.Count);
        
        Card cardToReturn = deckCards[randomDeckIndex];
        
        deckCards.Remove(cardToReturn);
        addCardToHand(cardToReturn);
    }

    public List<Card> getRandomCardsFromDeck(int cardAmount) { //gets amount cards from deck and puts it into hand. Returns cardsInHand
        for (int i = 0; i < cardAmount; i++) {
            drawCardFromDeck();
        }

        return cardsInHand;        
    }

    public void addCardToHand(Card card) {
        cardsInHand.Add(card);  
        cardHandUpdateEvent(cardsInHand); //show in UI
    }

    void initDeck() { // instantiates every card from the deck into the unity scene
        List<Card> cardDeck2 = getDeck();

        List<Card> instantiatedCards = new List<Card>();
        Vector3 cardTempPosition = new Vector3( 600, 600, 0); 

        foreach ( Card car in cardDeck2 ) {
            instantiatedCards.Add( Instantiate(car, cardTempPosition, transform.rotation ) );
        }

        deckCards = instantiatedCards;
    }

    public void moveCardsFromUsedDeckToPrimaryDeck() {
        if (usedDeckCards.Count == 0) { //avoid stupid error
            return;
        }

        List<Card> usedDeckCardsTemp = new List<Card>();

        foreach (Card c in usedDeckCards ) {
            usedDeckCardsTemp.Add(c);
        }

        foreach(Card card in usedDeckCardsTemp ) {
            usedDeckCards.Remove(card);
            addCardToDeck(card);
        }
        usedDeckCards.Clear();
}

    public void moveCardsFromHandToDeck() {
        foreach ( Card card in cardsInHand ) {
            addCardToDeck(card);
            card.transform.position = new Vector3(555, 555, 0);
        }
        cardsInHand.Clear();
    }

    public List<Card> getAllAvailableCards() {
        return availableCards;
    } 
}

