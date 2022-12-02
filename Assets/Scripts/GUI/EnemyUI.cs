using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

    [SerializeField] private Monster enemy;

    public int health; 
    private int fullHealth; 

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    //health bar
    public Slider healthSlider;
    public Color healthColor;
    private GameObject healthBarPosition;


    //spawn positions
    Dictionary<GameObject, Vector3> spawnPositions;



    void Awake() {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();

        healthColor = Color.red;

        spriteRenderer.sortingOrder = 2;
        healthBarPosition = GameObject.Find("EnemyPositionController"); 
    }

    void Update() {
        if (enemy == null || spriteRenderer == null) {
            spriteRenderer.enabled = false;
        }
        
        if ( health > 0 )
            updateGUI();

        updateHealthBar();
    }


    void init() {
        spriteRenderer.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = new Vector3(650, 650, 0);
    
        boxCollider.size = spriteRenderer.size;
        health = enemy.GetComponent<Monster>().getHealth();

        spriteRenderer.enabled = true;
    } 

    public Monster getEnemyObject() {
        return enemy;
    }

    private void updateGUI() {
        health = enemy.GetComponent<Monster>().getHealth();
    }

    public int getHealth() {
        return health;
    }

    public void setEnemyObj( Monster enemyObj ) {
        if (enemyObj == null) {
            return;
        }
        Debug.Log("trying to set " + enemyObj.transform.name);
        enemy = enemyObj;
        init(); //all the components get init after setting the obj
        health = enemyObj.getHealth();

        //trash hp bar
        healthSlider.transform.localScale = new Vector3(spriteRenderer.size.x + spriteRenderer.size.x * 2.4f, spriteRenderer.size.y + spriteRenderer.size.y * 2.4f , 0);
        healthSlider.value = health;
        fullHealth = health;
    }

    private void updateHealthBar() {
        float offset = 0.5f;
        healthSlider.transform.position = new Vector3 (transform.position.x, healthBarPosition.transform.position.y - offset, 0);
        if ( health > 0 && fullHealth > 0 ) {
            float value = health * 100 / fullHealth;
            healthSlider.value = value / 100;
        } else {
            healthSlider.value = 0;
        }
    }

    private Monster getEnemyObj() {
        return enemy;
    }

}
