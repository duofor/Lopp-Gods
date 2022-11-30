using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour {

    Util util = new Util();    

    public delegate void CardUsed( Card card, RaycastHit2D hit );
    public static event CardUsed cardUseEvent;

    //main card sprite
    SpriteRenderer spriteRenderer;
    LineRenderer lineRenderer;
    LineRenderer lineRendererResetter;
    
    //should have also a miniIcon
    Color initialColor;

    int initialLayer;
    bool canTarget = false;
    bool canUseCard = false;

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
        lineRendererResetter = lineRenderer;
        //outline
    }

    void OnMouseOver() {
        selectCard();
    }

    void OnMouseExit() {
        deselectCard();
    }

    void OnMouseDown() {
        canTarget = true;
    }

    void Update() {
        if (canTarget) {
            canUseCard = false;
            target();
        } else if (canUseCard) {
            useCard();
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        return;
    }

    void target() {
        if (Input.GetMouseButtonDown(0)) {
            startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get start pos when click
        }

        if (Input.GetMouseButton(0)) {
            selectCard();

            //drawing line
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineRenderer.SetPosition( 0, new Vector3(startMousePos.x ,startMousePos.y, 0) );
            lineRenderer.SetPosition( 1, new Vector3(mousePos.x ,mousePos.y, 0) );

            RaycastHit2D hit = getTargetAtMouse();
            if (hit && hit.collider.name == "Enemy" ) {
            //apply some highlight...
            ///// Can generate 4 copies of the sprite, set them black, move them up down left right of the base sprite so we create
            /////an outline effect, then set teh color to wahtever
                
            }

        } else if ( Input.GetMouseButtonUp(0) && canTarget ) {
            deselectCard();
            
            RaycastHit2D hit = getTargetAtMouse();
            if (hit && hit.collider.name == "Enemy" ) {
                canTarget = false;
                canUseCard = true;

            // fire some event 
                cardUseEvent(this, hit);
            // Event scope 
            //     --> Send stuff to the target.(dmg, info. etc)
            //     Update deck cards.
            //     play card use animation(move a litte to front )
            //     play card ability animation(attack, def utility... w/e it is play it)
                

            } else {
                lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
                lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
            }
        


        } else {
            canTarget = false;
        }



    }

    private RaycastHit2D getTargetAtMouse() {
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
        return hit;
    }

    void selectCard() {
        spriteRenderer.color = Color.cyan; //change color
        spriteRenderer.sortingOrder = 10;
    }

    void deselectCard() {
        spriteRenderer.sortingOrder = initialLayer;
        spriteRenderer.color = initialColor;
    }

    public void useCard() {

        //this can play some animation....whatever... for now i leave it empty.

        //temp hack to remove the4 card from UI
        transform.position = new Vector3(555,555,0);
        //remove the line renderer from the screen.
        lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
        lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
    }
}
