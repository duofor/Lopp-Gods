using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

    public readonly string enemyUITag = "EnemyUI";
    public readonly string enemyFloorPosition = "EnemyFloorPosition";
    public readonly string usedCardsLocation = "UsedCardsLocation";
    public readonly string spawnPoint = "SpawnPoint";
    public readonly Vector3 defaultGUIActorsScale = new Vector3(160, 160, 0);

    public float monsterTransitionTimeSeconds = 0.1f;

    public GameObject findRandomTargetByTag(string tag) {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag(tag);

        if (otherObjects[0] != null ){
            var random = Random.Range(0, otherObjects.Length);
            return otherObjects[random];
        }
        //promt to crash the program.
        return null;
    }

    public GameObject findTargetByTagAndName(string tag, string name) {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject go in otherObjects) {
            if (go.name == name) {
                return go;
            }
        }

        //promt to crash the program.
        return null;
    }

    public List<GameObject> getAllObjectsWithTag(string tag) {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag(tag);
        List<GameObject> objs = new List<GameObject>();

        foreach (GameObject go in otherObjects) {
            objs.Add(go);
        }

        return objs;
    }

    public Vector3 getMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    /** USAGE 
    lineRenderer.SetPosition( 1, new Vector3(hit.transform.position.x ,hit.transform.position.y, 0) ); // set the line to the target
    Vector3 middleOfLine = new Vector3( 
        (startMousePos.x - hit.transform.position.x) / 2,
        (startMousePos.y - hit.transform.position.y) / 2,
        0
    );
    util.curveMyLine(startMousePos, middleOfLine, hit.transform.position, lineRenderer );
    **/
    public void curveMyLine(Vector3 point1, Vector3 point2, Vector3 point3, LineRenderer lineRenderer) {
        int vertexCount = 12;
        var pointList = new List<Vector3>();
        for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount) {
            var tangentLineVertex1 = Vector3.Lerp(point1, point2, ratio);
            var tangentLineVertex2 = Vector3.Lerp(point2, point3, ratio);
            var bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
            pointList.Add(bezierpoint);
        }
        lineRenderer.positionCount = pointList.Count;
        lineRenderer.SetPositions(pointList.ToArray());
    }

    public EnemyUI getNextEnemyUIRef() {
        for (int i = 1; i < 10; i++) {
            string enemyUIName = "EnemyUI_" + i.ToString();
            GameObject enemyUI = GameObject.Find(enemyUIName);
            
            if (enemyUI == null)
                return null;
            
            EnemyUI enemyUIScript = enemyUI.GetComponent<EnemyUI>();
            Monster enemyObj = enemyUIScript.getEnemyObject();
            
            if (enemyUIScript && enemyObj != null ) {
                continue;
            }

            return enemyUIScript;
        }

        return null;
    } 

    public List<EnemyUI> getMonstersInScene() {
        List<GameObject> enemyGameObjects = getAllObjectsWithTag(enemyUITag);
        List<EnemyUI> monstersUIInScene = new List<EnemyUI>();
        foreach ( GameObject go in enemyGameObjects ) {
            Monster monster = go.GetComponent<EnemyUI>().getEnemyObject();
            if (monster == null) {
                continue;
            }
            monstersUIInScene.Add( go.GetComponent<EnemyUI>() );
        }

        return monstersUIInScene;
    }

    public Page getBattleGroundPage() {
        Page page = GameObject.Find("BattleGround").GetComponent<Page>();
        
        return page;
    }


    public RaycastHit2D getTargetAtMouse() {
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
        return hit;
    }
}
