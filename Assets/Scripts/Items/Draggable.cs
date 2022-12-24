using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour {
    Util util = new Util();

    Vector3 initialPosition;
    Vector3 defaultPosition = new Vector3(0,0,0);
    Vector3 mousePosition = new Vector3(0,0,0);

    BoxCollider2D box;

    Vector3 initialBoxColliderSize;

    void OnMouseDown() {
        initialPosition = transform.position;

        box = transform.GetComponent<BoxCollider2D>();
        initialBoxColliderSize = box.size;
        box.size = new Vector3(0,0,0);
    }

    void OnMouseUp() {
        /*  raycast to place where dropped.
        if inventory assign it
        if not inventory, put it back where it came from */

        int found = 0;
        RaycastHit2D hit = getTargetAtMouse();

        if (hit && hit.transform.tag == util.inventoryWeaponSlotTag ) {
            Debug.Log("this is what");
            WeaponSlot weaponSlot = hit.transform.GetComponent<WeaponSlot>();

            //check if we already have something equipped before trying to add a weap
            if ( weaponSlot.getWeapon() == null ) { 
                weaponSlot.setWeapon(this);
                transform.position = hit.transform.position;
                found = 1;
            }
        }   

        if ( found == 0 ) {
            transform.position = initialPosition;
        } else {

        }

        initialPosition = defaultPosition;
        box.size = initialBoxColliderSize;
    }

    void Update() {
        if ( initialPosition != defaultPosition ) {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //get start pos when click
            transform.position = mousePosition;
        }
    }

    private RaycastHit2D getTargetAtMouse() {
        Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
        return hit;
    }
}