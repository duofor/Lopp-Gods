using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour {
    // Start is called before the first frame update
    Util util = new Util();
    
    public List<Card> cards {get; set;}
    public Transform start; // initial card position
    [SerializeField] private GameObject tiltCenter;
    private Deck deck;

    float gapFromOneItemToTheNextOne; //gap needed between each card
    int lastShuffleCardCounter = 0; //counter to know how many cards we shuffled last time.
    float initialPosition_X; // initial position is used to always move cards towards the center

    [SerializeField] private GameObject cardPrefab; // used for testing

    void Start() {
        gapFromOneItemToTheNextOne = 0.5f;
        initialPosition_X = transform.position.x;
    }


    void Update() {
        Deck.deckWasUpdated += initCards; 
        if (cards == null || cards.Count == 0) //if list is null, stop function
            return;
        
        updateCardPositions();
        applyTilt();
    }

    void initCards(List<Card> cardsFromDeck) {
        cards = cardsFromDeck;
    }

    public void updateCardPositions() {


        //first we want to arrange them one near the other
        //first init for ==0                   if a new card was added since last shuffle
        float mediumPositionX = 0.0f;

        if ( lastShuffleCardCounter == 0 || lastShuffleCardCounter != cards.Count ) {
            lastShuffleCardCounter = cards.Count; 
            float count = 0.0f;

            foreach (Card go in cards) {
                go.transform.position = start.position; //relocating my card to the Start Position
                go.transform.position += new Vector3 (( count * gapFromOneItemToTheNextOne), 0, 0); // Moving my card 1f to the right
                count += 1f;
                mediumPositionX += go.transform.position.x;
            }

            if (cards.Count <= 2 ) { // its good enough
                return;
            }
            //basically we spawn an object, set it into the middle of the cards, calculate offset vs Hand Starting position, the temp obj to drag other cards to the offset.
            //we switch parents so we can drag the whole group.
            GameObject temp = new GameObject();
            GameObject tempPositionAdjuster = Instantiate(temp, new Vector3(mediumPositionX / cards.Count, transform.position.y, transform.position.z), transform.rotation );

            foreach (Card go in cards) {
                go.transform.parent = tempPositionAdjuster.transform;
            }
            float offset = transform.position.x + tempPositionAdjuster.transform.position.x / (2.0f * cards.Count / 6);
            tempPositionAdjuster.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);

            foreach (Card go in cards) {
                go.transform.parent = transform;
            }

            Destroy(tempPositionAdjuster); //cleanup leftover temp objs
            Destroy(temp);
        
        }
    }

    private void applyTilt() {
        Vector3 direction = tiltCenter.transform.position;

        foreach ( Card card in cards ) {
            if (card == null) 
                continue;
                
            Vector3 lookDir = direction - card.transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            card.transform.rotation = Quaternion.Slerp(card.transform.rotation, rotation, 50.0f * Time.deltaTime);
        }
    }


}
