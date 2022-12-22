using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    // Util util = new Util();    

    // public delegate void CardUsed( Card card, RaycastHit2D hit, int damage );
    // public static event CardUsed cardUseEvent;

    // //main card sprite
    // SpriteRenderer spriteRenderer;
    // LineRenderer lineRenderer;
    // LineRenderer lineRendererResetter;
    
    // //should have also a miniIcon
    // Color initialColor;

    // int initialLayer;
    // bool canTarget = false;
    // bool canUseCard = false;

    // // cards comming from rewards have this enabled. We dont want to be able to interact with the card script while this in in the UI
    // public bool isUIOnly; 

    // Vector3 startMousePos;
    // Vector3 mousePos;

    // Ray ray;
    // RaycastHit hit;

    // Vector3 usedCardsLocation;

    // void Start() {
    //     spriteRenderer = GetComponent<SpriteRenderer>();
    //     initialLayer = spriteRenderer.sortingOrder;
    //     initialColor = spriteRenderer.color;

    //     // line draw
    //     lineRenderer = GetComponent<LineRenderer>();
    //     lineRendererResetter = lineRenderer;
    //     //outline

    //     //getting the position for the used card deck
    //     usedCardsLocation = GameObject.Find(util.usedCardsLocation).transform.position;
    // }

    // void OnMouseOver() {
    //     selectCard();
    // }

    // void OnMouseExit() {
    //     deselectCard();
    // }

    // void OnMouseDown() {
    //     if (isUIOnly) 
    //         return;

    //     canTarget = true;
    // }

    // void Update() {
    //     if (canTarget) {
    //         canUseCard = false;
    //         target();
    //     } else if (canUseCard) {
    //         useCard();
    //     }

    // }

    // void OnCollisionEnter2D(Collision2D collision) {
    //     return;
    // }

    // void target() {
    //     if (Input.GetMouseButtonDown(0)) {
    //         startMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get start pos when click
    //     }

    //     if (Input.GetMouseButton(0)) {
    //         selectCard();

    //         //drawing line
    //         mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //         lineRenderer.SetPosition( 0, new Vector3(startMousePos.x ,startMousePos.y, 0) );
    //         lineRenderer.SetPosition( 1, new Vector3(mousePos.x ,mousePos.y, 0) );
    //     } else if ( Input.GetMouseButtonUp(0) && canTarget ) {
    //         deselectCard();
            
    //         RaycastHit2D hit = getTargetAtMouse();
    //         if (hit && hit.collider.tag == util.enemyUITag ) {

    //         // fire some event 
    //             int cardDamage = 1;
                
    //             if ( hit.transform.gameObject == null ) {
    //                 Debug.LogError(hit + "  is null ");
    //             } else {
    //                 if ( hit.transform.GetComponent<EnemyUI>().getEnemyObject() != null ) {
    //                     cardUseEvent(this, hit, cardDamage);
    //                     canUseCard = true;
    //                     canTarget = false;

    //                     StartCoroutine(cardUsedAnimation()); // icter animation
    //                 } else {
    //                     lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
    //                     lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
    //                 }
    //             }

    //         } else {
    //             lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
    //             lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
    //         }
        


    //     } else {
    //         canTarget = false;
    //     }



    // }

    // private RaycastHit2D getTargetAtMouse() {
    //     Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    //     RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
    //     return hit;
    // }

    // void selectCard() {
    //     spriteRenderer.color = Color.cyan; //change color
    //     spriteRenderer.sortingOrder = 10;
    // }

    // void deselectCard() {
    //     spriteRenderer.sortingOrder = initialLayer;
    //     spriteRenderer.color = initialColor;
    // }

    // private void useCard() {
    //     //remove the line renderer from the screen.
    //     lineRenderer.SetPosition( 0, new Vector3(555 ,555, 0) );
    //     lineRenderer.SetPosition( 1, new Vector3(555 ,555, 0) );
    // }

    // IEnumerator cardUsedAnimation() { // its just a dummy shit
    //     //We could get the used cards location and do a lerp animation with a zoom on it.
    //     //after that hsit is done we can either :
    //         //set the position of the card to a temp position
    //         //figure out some other interaction where the card is not displayed on screen anymore
    //     Vector3 initialCardPos = transform.position; 
    //     float timeElapsed = 0f;
    //     float lerpDuration = 1f;

    //     while ( timeElapsed < lerpDuration ) {
    //         transform.position = Vector3.Lerp(initialCardPos, usedCardsLocation, timeElapsed / lerpDuration);
            
    //         //WIP
    //         // transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, timeElapsed);
            
    //         timeElapsed += Time.deltaTime;
    //         if ( usedCardsLocation == transform.position ) {
    //             yield break;
    //         }
    //         yield return null;
    //     }

    //     transform.position = new Vector3(555,555,0); // use card hack. We only remove the card from teh screen when we use it.
    //     // transform.localScale = Vector3.one;

    // }
}
