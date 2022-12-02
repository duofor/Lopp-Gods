using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPositionController : MonoBehaviour {
    Util util = new Util();

    Image image;
    Vector3 initialPosition;

    void Awake() {
        image = GetComponent<Image>();
        initialPosition = transform.position;
    }

    void Update() {
        arrangeEnemies();
        
    }

    private List<GameObject> getAllActiveEnemyUIs() {
        List<GameObject> enemyUIs = util.getAllObjectsWithTag(util.enemyUITag);

        List<GameObject> activeEnemyUIs = new List<GameObject>();

        foreach ( GameObject go in enemyUIs ) {
            if ( go.GetComponent<EnemyUI>().getEnemyObject() == null )
                continue;
            
            activeEnemyUIs.Add(go);
        }

        return enemyUIs;
    }

    void arrangeEnemies() {

        List<GameObject> enemieUIs = getAllActiveEnemyUIs();

        float gapFromOneItemToTheNextOne = 0f; //first obj set normally, next use distance. We also use this as a first time counter
        float firstPosRef_Y = 0f;
        foreach (GameObject go in enemieUIs) {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            float xSize = spriteRenderer.bounds.size.x;
            float ySize = spriteRenderer.bounds.size.y;
            if (firstPosRef_Y == 0f) {
                firstPosRef_Y = (spriteRenderer.bounds.size.y / 2) + initialPosition.y;
            }

            if (gapFromOneItemToTheNextOne == 0f) { //first mob position hack
                go.transform.position = new Vector3(xSize, initialPosition.y , 0); //relocating my card to the Start Position
            } else {
                go.transform.position = new Vector3(xSize + (xSize * 0.2f), firstPosRef_Y , 0); //relocating my card to the Start Position
            }
            
            gapFromOneItemToTheNextOne = 1.5f;
            // mediumPositionX += go.transform.position.x;
        }
    }


}