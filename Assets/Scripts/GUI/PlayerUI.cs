using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private Slider healthSlider;
    
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    GameObject healthBarPosition;
    public int maxHealth;

    void Awake() {
        if (boxCollider == null ) {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        if (spriteRenderer == null) {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();

        }
        
        transform.localScale = new Vector3(650, 650, 0);
        
        spriteRenderer.sprite = player.GetComponent<SpriteRenderer>().sprite;
        boxCollider.size = spriteRenderer.size;
        healthBarPosition = GameObject.Find("EnemyPositionController"); 
    }

    void Update() {
        if ( maxHealth == 0 ) {
           maxHealth = GameController.instance.player.getMaxHealth();
        }

        bool fightState = GameController.instance.player.isInBattle;
        if ( fightState == false ) {
            spriteRenderer.enabled = false;
        } else {
            spriteRenderer.enabled = true;
            spriteRenderer.sortingOrder = 3;
            updateHealthBar();
        }
    }

    private void updateHealthBar() {
        float offset = 0.5f;
        healthSlider.transform.position = new Vector3 (transform.position.x, healthBarPosition.transform.position.y - offset, 0);
        int health = GameController.instance.player.getHealth();

        if ( health > 0 ) {
            float value = health * 100 / maxHealth;
            healthSlider.value = value / 100;
        } else {
            healthSlider.value = 0;
        }
    }
}
