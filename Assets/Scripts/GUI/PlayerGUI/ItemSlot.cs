using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlot : MonoBehaviour { 

    public abstract void setItem(Draggable i);

    public abstract void clearItem();

    public abstract Draggable getItemInSlot();

    public abstract void clearSkills();
}