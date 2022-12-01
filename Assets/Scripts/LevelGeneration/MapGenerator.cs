using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    // Start is called before the first frame update
    Util util = new Util();

    Vector3 cameraPosition;
    [SerializeField] private GameObject tile; 
    [SerializeField] private GameObject spawnPointIndicator; // to use later 
    
    private GameObject spawnPoint;
    private SpriteRenderer tileSpriteRenderer;
    private GameObject lastTileSpawned;
    private GameObject parentObject;

    int floorNumber = 1;

    void Start() {

    }

    void Update() {

    }

    public void init() {
        // cameraPosition = Camera.main.transform.position;
        parentObject = new GameObject();
        parentObject.name = "LoopRoad";
        parentObject.transform.parent = util.findTargetByTagAndName("GameHandler", "GameHandler").transform;

        var screenBottomCenter = new Vector3(80, 80, 0);
        cameraPosition = Camera.main.ScreenToWorldPoint(screenBottomCenter);
        
        tileSpriteRenderer = tile.GetComponent<SpriteRenderer>();
        spawnPoint = Instantiate(spawnPointIndicator, cameraPosition, Camera.main.transform.rotation );
        spawnPoint.transform.parent = parentObject.transform;
        lastTileSpawned = spawnPoint;
    }

    GameObject generateTile(int direction) {
        Vector3 tileSize = tileSpriteRenderer.bounds.size;
        Vector3 tilePosition = lastTileSpawned.transform.position;
        Vector3 spawnPosition = new Vector3(0, 0, 0);

        // 1 - up    2- down        3-left   4 - right
        if (direction == 1) {
            spawnPosition = tilePosition + new Vector3(0, tileSize.y, 0);
        } else if (direction == 2) {
            spawnPosition = tilePosition + new Vector3(0, -tileSize.y, 0);
        } else if (direction == 3) {
            spawnPosition = tilePosition + new Vector3(-tileSize.x, 0, 0);
        } else if (direction == 4) {
            spawnPosition = tilePosition + new Vector3(tileSize.x, 0, 0);
        } else {
            //we dont wanna end up here
            Debug.Log("error generating new tile");
            return null;
        }

        Collider[] intersecting = Physics.OverlapSphere(spawnPosition, 0.01f);
        if (intersecting.Length != 0) {
            Debug.Log("shit intersected with some other shit");
            return null;
        }

        tile.name = "floor" + floorNumber;
        floorNumber += 1;

        if (spawnPosition == spawnPoint.transform.position && floorNumber > 5) {
            return null;
        }
        GameObject newTile = Instantiate(tile, spawnPosition, transform.rotation);
        newTile.transform.parent = parentObject.transform;
        
        lastTileSpawned = newTile;


        return newTile;
    }

    public void createLevel1() {
        //Level 1... later to be moved into own class
        lastTileSpawned = generateTile(1);
        lastTileSpawned = generateTile(1);
        lastTileSpawned = generateTile(1);
        lastTileSpawned = generateTile(3);
        lastTileSpawned = generateTile(3);
        lastTileSpawned = generateTile(1);
        lastTileSpawned = generateTile(3);
        lastTileSpawned = generateTile(3);
        lastTileSpawned = generateTile(2);
        lastTileSpawned = generateTile(2);
        lastTileSpawned = generateTile(2);
        lastTileSpawned = generateTile(2);
        lastTileSpawned = generateTile(4);
        lastTileSpawned = generateTile(4);
        lastTileSpawned = generateTile(4);
    }

    public void generateRandomLevel(int size) {
        int wentUp = 0;
        int random = 0;;
        int safetyBreak = 1;
        
        while ( wentUp < size + 3) {
            if (safetyBreak >= 150) { break; }
            safetyBreak += 1;

            random = Random.Range(1,4);
            if (random == 1 || random == 2) {
                wentUp += 1;
                generateTile(1); //up
            } else {
                generateTile(3); // left
            }
        }       

        generateTile(1); //up. We want the last tile to be up so we can go right then down
        generateTile(4);
        generateTile(4);
        generateTile(4);
        if (random == 1) { // chance to create a lucky loop
            generateTile(1); //up. We want the last tile to be up so we can go right then down
            generateTile(1); //up. We want the last tile to be up so we can go right then down
        }
        generateTile(4);
        generateTile(4);
        generateTile(2); // go down
        generateTile(2); // go down
        generateTile(4);
        generateTile(4);

        ;

        safetyBreak = 1;
        while ( lastTileSpawned.transform.position.y != spawnPoint.transform.position.y ) {
            if (safetyBreak >= 150) { break; }
            safetyBreak += 1;
            random = Random.Range(1,4);
            if (random == 1) {
                if ( lastTileSpawned.transform.position.y == (spawnPoint.transform.position.y + tileSpriteRenderer.bounds.size.y) ) {
                    continue;
                }
                generateTile(4);
                generateTile(2);
            } else {
                generateTile(2);
            }
        }

        safetyBreak = 1;
        while ( lastTileSpawned.transform.position.x != spawnPoint.transform.position.x ) {
            if (safetyBreak >= 30) { break; }
            safetyBreak += 1;
            
            generateTile(3);
        }

        Camera.main.transform.position = getMiddleOfRoadPosition();
    }

    Vector3 getMiddleOfRoadPosition() {
        GameObject[] allMovePoints = GameObject.FindGameObjectsWithTag("Floor");

        float x = 0f;
        float y = 0f;

        foreach ( GameObject go in allMovePoints ) {
            x += go.transform.position.x;
            y += go.transform.position.y;
        }

        Vector3 centerPosition = new Vector3(x / allMovePoints.Length, y / allMovePoints.Length, 0);
        
        return centerPosition;
    }   

    public GameObject getRandomTile() {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag("Floor");

        int random = Random.Range(0, otherObjects.Length);

        return otherObjects[random] ? otherObjects[random] : null;
    }
}
