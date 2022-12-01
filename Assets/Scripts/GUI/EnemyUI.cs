using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {

    [SerializeField] private GameObject enemy;

    public int health; 

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    //health bar
    public Slider healthSlider;
    public Color healthColor;

    void Awake() {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();

        Monster.monsterTakeDamageUpdateUI += updateGUI;
    }

    void Update() {
        if (enemy == null || spriteRenderer == null) {
            spriteRenderer.enabled = false;
        }

        //trash hp bar
        healthSlider.transform.position = transform.position - new Vector3 (0, spriteRenderer.size.y * 2.4f , 0);
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

        //trash hp bar
        healthSlider.transform.localScale = new Vector3(spriteRenderer.size.x + spriteRenderer.size.x * 2.4f, spriteRenderer.size.y + spriteRenderer.size.y * 2.4f , 0);
    }

    private void createHealthBar() {

    }

    public void updateHealthBar() {
        healthSlider.value = health;
        // healthSlider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low, high, healthSlider.normalizedValue);

        if (healthSlider.value < 1) {
            healthSlider.fillRect.GetComponentInChildren<Image>().color = new Color(0, 0, 0, 0);
        }
    }

}
