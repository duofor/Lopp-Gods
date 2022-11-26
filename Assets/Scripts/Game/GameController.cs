using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : MonoBehaviour {
    // Start is called before the first frame update
    // [SerializeField] public MapGenerator mapGenerator;

    public PlayerController player;
    public MapGenerator mapGenerator;
    public Monster monster;
    public GUIController guiController;
    public Deck deck;
    public CardPosition cardPosition;

    [SerializeField] private Card testCard = null;

    private Util util = new Util();

    void Awake() {
        Monster.deathEvent += rewardPlayerWithCard; 
    }

    void Start() {
        // mapGenerator.init();
        // mapGenerator.generateRandomLevel(4);

        player.init();
        player.spawnPlayer();

        GameObject randomTile = mapGenerator.getRandomTile();
        monster.spawnAtLocation(randomTile.transform.position);

        //make gui camera same as main camera.
        guiController.guiCamera.transform.position = Camera.main.transform.position;
        guiController.guiCamera.transform.localScale = Camera.main.transform.localScale;
    }

    void Update() {

        if( util.getAllObjectsWithTag("Monster").Length != 0 ) {
            //we have monsters in the game so we continue looping
            player.loop();
        } else {
            //spawn one monster
            GameObject randomTile = mapGenerator.getRandomTile();
            monster.spawnAtLocation(randomTile.transform.position);
        }
    }

    void rewardPlayerWithCard() { // maybe this should not be an event.
        //add a card to deck
        if (testCard && deck) {
            if ( cardPosition.cards.Count > 15 ) { //its max bro
                // make the card go poof
                return;
            } 

            Card cardGameObject = Instantiate(testCard, deck.transform.position, deck.transform.rotation);
            cardPosition.cards.Add(cardGameObject.gameObject);

            // deck.addCardToDeck(cardGameObject);
        }

        Debug.Log("I should receive a CARD for killing you");
    }
}
