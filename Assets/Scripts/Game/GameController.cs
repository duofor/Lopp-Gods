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

    private Util util = new Util();

    void Start() {
        mapGenerator.init();
        mapGenerator.generateRandomLevel(4);

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
            GameObject randomTile = mapGenerator.getRandomTile();
            monster.spawnAtLocation(randomTile.transform.position);
            //spawn one monster else
        }
    }
}
