using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    //main card sprite
    SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;
    //should have also a miniIcon
    Color initialColor;

    int initialLayer;
    bool canTarget = false;

    Vector3 startMousePos;
    Vector3 mousePos;

    Ray ray;
    RaycastHit hit;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialLayer = spriteRenderer.sortingOrder;
        initialColor = spriteRenderer.color;

        // line draw
        lineRenderer = GetComponent<LineRenderer>();
        //outline
    }

    void OnMouseOver() {
        spriteRenderer.sortingOrder = 10;
        spriteRenderer.color = Color.cyan;
    }

    void OnMouseExit() {
        spriteRenderer.sortingOrder = initialLayer;
        spriteRenderer.color = initialColor;
    }

    void OnMouseDown() {
        canTarget = true;
    }

    void Update() {
        if (canTarget) {
            target();
        }

    }

    public void useCard() {
        Destroy(transform.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        return;
    }

    void target() {
        if (Input.GetMouseButtonDown(0)) {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get start pos when click
        }

        if (Input.GetMouseButton(0)) {
            //drawing line
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition( 0, new Vector3(startMousePos.x ,startMousePos.y, 0) );
            lineRenderer.SetPosition( 1, new Vector3(mousePos.x ,mousePos.y, 0) );

        } else if (canTarget && Input.GetMouseButtonUp(0)) {
            //Line released, getting targeted object.
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
            if (hit) {
                //if release do some magic.

                //apply some highlight...
                ///// Can generate 4 copies of the sprite, set them black, move them up down left right of the base sprite so we create
                /////an outline effect, then set teh color to wahtever



                print(hit.transform.gameObject.tag);
            }

        } else {
            canTarget = false;
            lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
            lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
        }



    }
}
