using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Draggable {
    public List<Skill> weaponSkills; 
    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public List<Skill> getWeaponSkills() {
        return weaponSkills;
    }
}