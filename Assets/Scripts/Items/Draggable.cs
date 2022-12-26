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
    ItemSlot previousItemSlot;


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
        ItemSlot currentItemSlot = null;
        RaycastHit2D hit = getTargetAtMouse();
        
        if (hit && hit.transform.tag == util.inventoryWeaponSlotTag ) { //set in weapon slot
            ItemSlot inventorySlot = hit.transform.GetComponent<WeaponSlot>();
            found = addItemToSlot(transform.gameObject, inventorySlot);
            currentItemSlot = inventorySlot;
        } else if ( hit && hit.transform.tag == util.inventoryItemSlotTag ) { //set in inventory slot
            ItemSlot inventorySlot = hit.transform.GetComponent<InventorySlot>();
            found = addItemToSlot(transform.gameObject, inventorySlot);
            currentItemSlot = inventorySlot;
        } else {
            if ( hit ) {
                Debug.Log("tried to put " + transform.name + " in " + hit.transform.name);
            }
        }

        if ( found == 0 ) {
            transform.position = initialPosition;
        } else {
            // if we registered an item to a new slot, delete item in previous slot.
            if ( currentItemSlot != null && currentItemSlot != previousItemSlot ) {
                
                if (previousItemSlot != null) {
                    previousItemSlot.clearItem();
                    if ( previousItemSlot.GetType() == typeof(WeaponSlot) ) {
                        previousItemSlot.clearSkills();
                    } 
                }
                previousItemSlot = currentItemSlot;
            }
        }

        initialPosition = defaultPosition;
        box.size = initialBoxColliderSize;
    }

    private int addItemToSlot(GameObject item, ItemSlot inventorySlot) {
        //check if we already have something equipped before trying to add a weap
        if (inventorySlot.getItemInSlot() == null) {
            Debug.Log("Placing " + item.transform.name + " in " + inventorySlot.ToString());
            inventorySlot.setItem(this);
            transform.position = inventorySlot.transform.position;
            return 1;
        }

        return 0;
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