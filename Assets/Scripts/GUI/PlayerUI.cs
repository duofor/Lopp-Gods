using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {


    [SerializeField] private GameObject player;
    
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

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
        
        gameObject.SetActive(true);
    }

    void Update() {
        if (GameController.instance.player.isInBattle == false) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);

        }
    }
}
