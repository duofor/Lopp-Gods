using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {
    //basic attributes
    int health,damage,level,defense = 0;
    BoxCollider2D boxCollider;


    void Start() {
        boxCollider = GetComponent<BoxCollider2D>();
        Debug.Log("BLllaarrhhhh spawned: " + transform.gameObject.name);
    }

    void Update() {
        
    }

    public GameObject spawnAtLocation(Vector3 location) {
        GameObject monster = Instantiate(transform.gameObject, location, transform.rotation);
        init(monster);
        
        return monster;
    }

    void init(GameObject gameObject) {
        // can be used to group spawned shit inside a parent object so that the hierarchy editor is clearer.
        //transform.parent = util.findTargetByTagAndName("GameHandler", "GameHandler").transform;
        
        //basic init stuff. tbd later
        health = 5;
        damage = 1;
        level = 1;
        defense = 1;
    }
}
