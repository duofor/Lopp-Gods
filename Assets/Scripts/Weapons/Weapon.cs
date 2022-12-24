using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public delegate void PlayerUIRegisterWeapon(Weapon weapon);
    public static event PlayerUIRegisterWeapon playerUIRegisterWeapon;

    public List<Skill> weaponSkills; 

    private SpriteRenderer spriteRenderer;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void equipWeapon() {
        playerUIRegisterWeapon(this); //can be moved to on pickup or whatever interaction
    }

    public List<Skill> getWeaponSkills() {
        return weaponSkills;
    }
}