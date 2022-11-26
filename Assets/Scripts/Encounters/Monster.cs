using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Monster : MonoBehaviour {
    //basic attributes
    int health,damage,level,defense = 0;
    BoxCollider2D boxCollider;

    [SerializeField] private GameObject droppedItem;

    public delegate void MonsterDeath();
    public static event MonsterDeath deathEvent;

    void Awake() {
        boxCollider = GetComponent<BoxCollider2D>();
        // Debug.Log("BLllaarrhhhh spawned: " + transform.gameObject.name);
    }

    void Update() {

    }

    void OnDestroy() {
        deathEvent();
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

    public GameObject dropItem() {
        //add a chance for this drop maybe
        GameObject drop = Instantiate(droppedItem, transform.position, transform.rotation);
        
        // Destroy(this.transform.gameObject);
    
        return drop;
    }

    void startEncounter() {
        //play some animation ....

        // show fighting ground.
    }
}
