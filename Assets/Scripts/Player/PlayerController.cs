using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Looper {
    Util util = new Util();

    [SerializeField] public PlayerUI playerUI;

    public int health;
    int maxHealth;
    public int loopNumber = 0;

    public delegate void PlayerUIHitEffect();
    public static event PlayerUIHitEffect playerUIHitEffect;

    GameObject[] allMovePoints;
    List<GameObject> floors = new List<GameObject>();
    Tile nextFloorToMove = null;
    Vector3 targetPosition;

    Rigidbody2D rb;    

    public bool isInBattle = false;
    
    float movementSpeed = 0.03f;
    // [SerializeField] float movementSpeed = 0.006f;
    int currentFloor = 1;
    bool shouldMove = true;

    public bool canEndTurn = false;

    public void init() {
        health = 10;
        maxHealth = 10;

        allMovePoints = GameObject.FindGameObjectsWithTag("MovePoint");
        rb = GetComponent<Rigidbody2D>();
        //init floors
        foreach (GameObject gameObject in allMovePoints) {
            GameObject parent = gameObject.transform.parent.gameObject;
            floors.Add(parent);
        }
    }

    //this method needs to stay in a separate class. Perhaps Looper
    public void loop() {

        if ( shouldMove ) {
            targetPosition = getNextFloorPosition();
        }

        if ( transform.position == targetPosition) {
            Tile currentTile = nextFloorToMove.GetComponent<Tile>();
            
            if ( currentTile.transform.name == util.spawnPoint ) {
                loopNumber += 1;
            }

            List<Monster> encounter = currentTile.getEncounter();
            if ( encounter.Count > 0 ) {
                beginBattle(encounter);
            } else {
                shouldMove = true;
            }
        } else {
            //move towards next tile.
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed );
        }

    }

    Vector3 getNextFloorPosition() {
        shouldMove = false;
        nextFloorToMove = getNextFloor();
        targetPosition = nextFloorToMove.transform.position;
                
        return targetPosition;
    }

    Tile getNextFloor() {
        if (currentFloor == allMovePoints.Length) {
            currentFloor = 0;
        }  
        
        string objectName = "floor" + currentFloor + "(Clone)";

        if (currentFloor == 0 ) {
            objectName = "SpawnPoint";
        }

        foreach (GameObject gameObject in floors) {
            if (gameObject.name == objectName ) {
                currentFloor += 1;
                return gameObject.GetComponent<Tile>();
            }
        }
        return null;
    }

    public void spawnPlayer() {
        GameObject spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint")[0];
        transform.position = spawnPoint.transform.position;
    }

    private void beginBattle(List<Monster> enemies) {
        //begins battle....
        foreach (Monster enemy in enemies ) {
            //set the enemy obj in one of the available UIs
            EnemyUI enemyUIScript = util.getNextEnemyUIRef();

            enemyUIScript.setEnemyObj(enemy); 
        }
        
        isInBattle = true;
    }

    public Tile getCurrentFloor() {
        return nextFloorToMove;
    }

    public int getHealth() {
        return health;
    }

    public void  setHealth(int h) {
        health = h;
    }

    public int getMaxHealth() {
        return health;
    }

    public void takeDamage(int damage) {
        health -= damage;
        playerUIHitEffect();
    }
}
