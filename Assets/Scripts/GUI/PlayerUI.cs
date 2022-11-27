using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] private GameObject player;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Awake() {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = player.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = new Vector3(650, 650, 0);
        
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.size = spriteRenderer.size;
    }

    


}
