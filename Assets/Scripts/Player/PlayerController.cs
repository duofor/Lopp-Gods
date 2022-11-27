using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Looper {
    // Start is called before the first frame update
    GameObject[] allMovePoints;
    List<GameObject> floors = new List<GameObject>();
    GameObject nextFloorToMove = null;
    Vector3 targetPosition;

    Rigidbody2D rb;    

    public bool isInBattle = false;
    
    float movementSpeed = 0.05f;
    // [SerializeField] float movementSpeed = 0.006f;
    int currentFloor = 1;
    bool shouldMove = true;
    
    public void init() {
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
            // Debug.Log("getting new position");
            shouldMove = true;
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

    GameObject getNextFloor() {
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
                return gameObject;
            }
        }
        return null;
    }

    public void spawnPlayer() {
        GameObject spawnPoint = GameObject.FindGameObjectsWithTag("SpawnPoint")[0];
        transform.position = spawnPoint.transform.position;
    }


    void OnCollisionEnter2D(Collision2D collision) {
        if ( collision.transform.tag != "Monster" ) {
            return;
        }
        //begin battle....
        isInBattle = true;

        // Destroy(collision.gameObject);
    }

    void finishBattle() {
        isInBattle = true;
    }
}
