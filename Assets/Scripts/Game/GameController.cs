using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : GameStateManager {
    private Util util = new Util();
    // Start is called before the first frame update
    // [SerializeField] public MapGenerator mapGenerator;
    public static GameController instance;

    public PlayerController player;
    public MapGenerator mapGenerator;
    public Monster monster;
    public GUIController guiController;
    public Deck deck; //this needs Editor Initialization for the beggining
    public CardPosition cardPosition;


    void Awake() {
        instance = this;
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
        if( util.getAllObjectsWithTag("Monster").Length == 0 ) {
            //spawn one monster
            GameObject randomTile = mapGenerator.getRandomTile();
            monster.spawnAtLocation(randomTile.transform.position);
        } 



    }

    public void initStartBattleCards(int cardAmount) {
        // if card does not have Trigger checked on BoxCollider. it will collide with fcking player

        List<Card> startingCards = deck.getRandomCardsFromDeck(cardAmount);


        // Debug.Log("I should receive a CARD for killing you");
    }
}
