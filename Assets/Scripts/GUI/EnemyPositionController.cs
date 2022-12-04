using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyPositionController : MonoBehaviour {
    Util util = new Util();

    Vector3 initialPosition;

    void Awake() {
        initialPosition = new Vector3(transform.position.x, transform.position.y, 0);
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

    public void arrangeEnemies() {
        // StartCoroutine( arrangePositions() );
        StartCoroutine(arrangePositions());
    }

    IEnumerator arrangePositions() {
        // Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
        List<EnemyUI> enemieUIs = util.getMonstersInScene();
        float gapFromOneItemToTheNextOne = 0f; //first obj set normally, next use distance. We also use this as a first time counter
        float firstPosRef_Y = 0f;

        foreach ( EnemyUI go in enemieUIs ) {
            go.transform.position = initialPosition;
        }

        float lerpDuration = 2.5f;
        float timeElapsed = 0f;
        while ( timeElapsed < lerpDuration ) {
            foreach (EnemyUI go in enemieUIs) {
                SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
                float xSize = spriteRenderer.bounds.size.x;
                float ySize = spriteRenderer.bounds.size.y;
                if (firstPosRef_Y == 0f) {
                    firstPosRef_Y = (spriteRenderer.bounds.size.y / 2) + initialPosition.y;
                }
                
                Vector3 restOfMobsPosition = new Vector3(xSize + (xSize / enemieUIs.Count ) + gapFromOneItemToTheNextOne, firstPosRef_Y , 0);
                Vector3 firstMobPosition = new Vector3(xSize, initialPosition.y , 0);


                    if (gapFromOneItemToTheNextOne == 0f) { //first mob position hack
                        go.transform.position = Vector3.Lerp(go.transform.position, firstMobPosition, timeElapsed / lerpDuration); //relocating my card to the Start Position
                    } else {
                        go.transform.position = Vector3.Lerp(go.transform.position, restOfMobsPosition, timeElapsed / lerpDuration ); //relocating my card to the Start Position
                    }
                    timeElapsed += Time.deltaTime;

                }
            gapFromOneItemToTheNextOne = transform.position.x / enemieUIs.Count / 2;
            yield return null;
        }
    }
}