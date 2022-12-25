using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : ItemSlot {
    Util util = new Util();

    [SerializeField] private Draggable item; //just so we can assign it in inspector 
    private SpriteRenderer itemSpriteRenderer;

    public override void setItem(Draggable i) {
        item = i;
    }
    public override void clearItem() {
        item = null;
    }
    public override void clearSkills() {}
    public override Draggable getItemInSlot() {
        return item;
    }

}
