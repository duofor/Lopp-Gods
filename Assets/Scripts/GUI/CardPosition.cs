using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPosition : MonoBehaviour {
    // Start is called before the first frame update
    Util util = new Util();
    
    public List<GameObject> cards;
    public Transform start; // initial card position
    
    float gapFromOneItemToTheNextOne; //gap needed between each card
    int lastShuffleCardCounter = 0; //counter to know how many cards we shuffled last time.
    float initialPosition_X; // initial position is used to always move cards towards the center


    [SerializeField] private GameObject cardPrefab;

    void Start() {
        gapFromOneItemToTheNextOne = 0.5f;
        initialPosition_X = transform.position.x;
        cards = initTestCards();
    }


    void Update() {
        updateCardPositions();
    }

    private List<GameObject> initTestCards() {
        List<GameObject> testCards = new List<GameObject>();
        
        for (int i = 0; i < 5; i++) {
            GameObject cardObj = Instantiate(cardPrefab, transform.position, transform.rotation);
            
            cardObj.transform.parent = transform;
            testCards.Add(cardObj);
        }

        return testCards;
    }
    public void updateCardPositions() {
        if (cards.Count == 0) //if list is null, stop function
            return;

        //first we want to arrange them one near the other
        //first init for ==0                   if a new card was added since last shuffle
        float mediumPositionX = 0.0f;

        if ( lastShuffleCardCounter == 0 || lastShuffleCardCounter != cards.Count ) {
            lastShuffleCardCounter = cards.Count; 
            float count = 0.0f;

            foreach (GameObject go in cards) {
                go.transform.position = start.position; //relocating my card to the Start Position
                go.transform.position += new Vector3 (( count * gapFromOneItemToTheNextOne), 0, 0); // Moving my card 1f to the right
                count += 1f;
                mediumPositionX += go.transform.position.x;
            }

            //basically we spawn an object, set it into the middle of the cards, calculate offset vs Hand Starting position, the temp obj to drag other cards to the offset.
            //we switch parents so we can drag the whole group.
            GameObject temp = new GameObject();
            GameObject tempPositionAdjuster = Instantiate(temp, new Vector3(mediumPositionX / cards.Count, transform.position.y, transform.position.z), transform.rotation );

            foreach (GameObject go in cards) {
                go.transform.parent = tempPositionAdjuster.transform;
            }
            float offset = transform.position.x + tempPositionAdjuster.transform.position.x / (2.0f * cards.Count / 6);
            tempPositionAdjuster.transform.position = new Vector3(transform.position.x + offset, transform.position.y, transform.position.z);

            foreach (GameObject go in cards) {
                go.transform.parent = transform;
            }

            Destroy(tempPositionAdjuster);
            Destroy(temp);
        }

    }

}
