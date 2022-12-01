using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Monster : MonoBehaviour {
    //basic attributes
    BoxCollider2D boxCollider;

    private int health = 2;

    public delegate void TakeDamage(int health);
    public static event TakeDamage monsterTakeDamageUpdateUI;

    public delegate void MonsterDeath();
    public static event MonsterDeath deathEvent;

    Vector3 initialPosition;

    void Awake() {
        initialPosition = transform.position;
        boxCollider = GetComponent<BoxCollider2D>();
        Card.cardUseEvent += attacked;
        // Debug.Log("BLllaarrhhhh spawned: " + transform.gameObject.name);
    }

    void Update() {
        if (health <= 0 ) {
            Destroy(gameObject);
        }
    }

    void OnDestroy() {
        Card.cardUseEvent -= attacked;
    }

    public GameObject spawnAtLocation(Vector3 location) {
        GameObject monster = Instantiate(transform.gameObject, location, transform.rotation);
        
        return monster;
    }

    void attacked(Card card, RaycastHit2D hit, int damage) {
        Debug.Log(hit.transform.name);
        takeDamage(damage);
        StartCoroutine( doSomeSmallShake( hit ) );
    }

    IEnumerator doSomeSmallShake( RaycastHit2D hit ) {
        Debug.Log("yeahh");
        Vector3 initialHitPosition = hit.transform.position;

        float timePassed = 0;
        bool flip = false;
        while (timePassed < 0.3f) {
            // Shake
            if (flip) {
                flip = !flip; 
                hit.transform.position += new Vector3(0, initialPosition.y / 80, 0);
                hit.transform.position += new Vector3(initialPosition.x / 80, 0, 0);
            } else {
                flip = !flip; 
                hit.transform.position -= new Vector3(0, initialPosition.y / 80, 0);
                hit.transform.position -= new Vector3(initialPosition.x / 80, 0, 0);
            }
            timePassed += Time.deltaTime;
            yield return null;
        }

        hit.transform.position = initialHitPosition;
    }

    void takeDamage(int damage) {
        this.health -= damage;
        monsterTakeDamageUpdateUI(health);
    }

    public void setHealth( int healthAmount ) {
        this.health = healthAmount;
    }

    public int getHealth() {
        return health;
    }
}
