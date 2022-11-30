using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour {

    [SerializeField] private GameObject enemy;

    public int health; 

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Awake() {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();

        Monster.monsterTakeDamageUpdateUI += updateGUI;
    }

    void Update() {
        if (enemy == null || spriteRenderer == null) {
            spriteRenderer.enabled = false;
        }
    }


    void init() {
        spriteRenderer.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = new Vector3(650, 650, 0);
    
        boxCollider.size = spriteRenderer.size;
        health = enemy.GetComponent<Monster>().getHealth();

        spriteRenderer.enabled = true;
    } 

    public GameObject getEnemyObject() {
        return enemy;
    }

    private void updateGUI(int healthUpdated) {
        this.health = healthUpdated;
    }

    public int getHealth() {
        return health;
    }

    public void setEnemyObj( GameObject enemyObj ) {
        if (enemyObj == null) {
            return;
        }
        Debug.Log("trying to set " + enemyObj.transform.name);
        enemy = enemyObj;
        init(); //all the components get init after setting the obj
        health = enemyObj.GetComponent<Monster>().getHealth();
    }

}
