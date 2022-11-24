using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util {
    void Start() {
    }

    void Update() {
    }

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
}
