using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUI : MonoBehaviour {

    [SerializeField] private GameObject enemy;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    Color initialColor;

    void Awake() {
        //doesnt work yet
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = new Vector3(650, 650, 0);
    
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = spriteRenderer.size;

        initialColor = spriteRenderer.color;
    }


    void OnMouseOver() {
        //highlight the fucker
        // spriteRenderer.color = Color.red;
    }

    void OnMouseExit() {
        //stop highlughting the fucker
        // spriteRenderer.color = initialColor;
    }

}
