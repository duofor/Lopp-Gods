using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {

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

    public GameObject[] getAllObjectsWithTag(string tag) {
        GameObject[] otherObjects = GameObject.FindGameObjectsWithTag(tag);

        return otherObjects;
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
}
