using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    
    public static readonly Inventory instance = new Inventory();

    private List<GameObject> inventorySlots; 
    
    void Awake() {
        inventorySlots = getInventorySlots();
        
        foreach (var slot in inventorySlots) {
            GameObject itemInsideSlot = new GameObject();
            itemInsideSlot.transform.parent = slot.transform;
            //<<<------- needs logic 
        }
    }

    void Update() {
    }

    /** inventory slots need to be named Inventory<lalalal>
    this returns a empty inventory list now......**/
    private List<GameObject> getInventorySlots() {
        int invSlotsCount = transform.childCount;
        var inventorySlotObjects = new List<GameObject>();

        for (int childNumber = 0; childNumber < invSlotsCount; childNumber++) {
            GameObject childObj = transform.GetChild(childNumber).gameObject;
            if ( childObj.name.StartsWith("Inventory") ) {
                inventorySlotObjects.Add( childObj );
            }
        }

        return inventorySlotObjects;
    }

    public List<GameObject> getInventory() {
        return inventorySlots;
    }

}

