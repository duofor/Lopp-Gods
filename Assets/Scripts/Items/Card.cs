using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    //main card sprite
    SpriteRenderer spriteRenderer;
    //should have also a miniIcon

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void acquire() {

    }

    public void useCard() {
        Destroy(transform.gameObject);
    }


}
