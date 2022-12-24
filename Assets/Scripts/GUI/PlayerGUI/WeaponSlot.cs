using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour {
    Util util = new Util();

    private Weapon weapon;
    private SpriteRenderer weaponSpriteRenderer;

    void Update() {
        
    }

    public void setWeapon(Draggable wep) {
        weapon = (Weapon) wep;
        GameController.instance.playerUI.setPlayerWeapon(weapon); //astronomic hack
    }

    public Weapon getWeapon() {
        return weapon;
    }
}
