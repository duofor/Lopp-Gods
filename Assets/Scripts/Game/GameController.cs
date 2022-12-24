using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameController : GameStateManager {
    private Util util = new Util();
    // Start is called before the first frame update
    // [SerializeField] public MapGenerator mapGenerator;
    public static GameController instance;

    //assigned in editor for ease.
    public PlayerController player;
    public MapGenerator mapGenerator;
    public GUIController guiController;
    public EnemyPositionController enemyPositionController;
    public ActionManager actionManager;
    public MonsterController monsterController;
    public MenuController menuController;
    public PlayerSkillManager playerSkillManager;



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
        if( util.getAllObjectsWithTag("Monster").Count < 2 ) {
            //spawn one monster
            if (monsterPool.Count == 0 )
                return;
                
            GameObject randomTile = mapGenerator.getRandomTile();

            Debug.Log(randomTile);


            Monster monsterToSpawn = monsterPool[0];
            Monster monsterToSpawn2 = monsterPool[1];

            randomTile.GetComponent<Tile>().addEncounter(monsterToSpawn);
            randomTile.GetComponent<Tile>().addEncounter(monsterToSpawn2);
            Debug.Log("Spawning some trash");
        } 



    }
}
