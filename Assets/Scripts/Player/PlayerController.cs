using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Looper {
    Util util = new Util();

    [SerializeField] public PlayerUI playerUI;

    public int health;
    int maxHealth;

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

    public void init() {
        health = 10;
        maxHealth = 10;

        allMovePoints = GameObject.FindGameObjectsWithTag("MovePoint");
        rb = GetComponent<Rigidbody2D>();
        //init floors
        foreach (GameObject gameObject in allMovePoints) {
            GameObject parent = gameObject.transform.parent.gameObject;
            // Debug.Log(parent.name);
            floors.Add(parent);
        }
    }

    public void loop() {

        if ( shouldMove ) {
            targetPosition = getNextFloorPosition();
        }

        if ( transform.position == targetPosition) {
            List<Monster> encounter = nextFloorToMove.GetComponent<Tile>().getEncounter();
            if ( encounter.Count > 0 ) {
                beginBattle(encounter);
            } else {
                shouldMove = true;
            }
        } else {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed );
            // rb.MovePosition(targetPosition * movementSpeed );
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
            objectName = "Spawn Point(Clone)";
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

    public void finishBattle() {
        isInBattle = false;
    }

    private void beginBattle(List<Monster> enemies) {
        //begins battle....
        foreach (Monster enemy in enemies ) {
            //set the enemy obj in one of the available UIs
            EnemyUI enemyUIScript = util.getNextEnemyUIRef();

            Debug.Log(enemyUIScript.name);

            enemyUIScript.setEnemyObj(enemy); 
        }
        
        isInBattle = true;
    }

    public Tile getNextFloorToMove() {
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
