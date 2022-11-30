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
    public GUIController guiController;
    public Deck deck; //this needs Editor Initialization for the beggining
    public CardPosition cardPosition;

    //nu are ce cauta aici
    public List<Monster> monsterPool; // make a pool of monsters.

    void Awake() {
        instance = this;

        
    }

    void Start() {
        // mapGenerator.init();
        // mapGenerator.generateRandomLevel(4);

        player.init();
        player.spawnPlayer();

        //make gui camera same as main camera.
        guiController.guiCamera.transform.position = Camera.main.transform.position;
        guiController.guiCamera.transform.localScale = Camera.main.transform.localScale;

    }

    void Update() {
        if( util.getAllObjectsWithTag("Monster").Length < 2 ) {
            //spawn one monster
            GameObject randomTile = mapGenerator.getRandomTile();
            Monster monsterToSpawn = monsterPool[0];
            monsterToSpawn.spawnAtLocation(randomTile.transform.position);

            monsterPool.Remove(monsterPool[0]);
        } 



    }

    public void initStartBattleCards(int cardAmount) {
        // if card does not have Trigger checked on BoxCollider. it will collide with fcking player

        List<Card> startingCards = deck.getRandomCardsFromDeck(cardAmount);


        // Debug.Log("I should receive a CARD for killing you");
    }
}
