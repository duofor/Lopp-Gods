using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : ItemSlot {
    Util util = new Util();

    private Draggable weapon;
    private SpriteRenderer weaponSpriteRenderer;

    List<Skill> instantiatedSkills;

    public override void setItem(Draggable wep) {
        GameController.instance.playerUI.setPlayerWeapon(wep); //astronomic hack
        disableBoxCollider();
        wep.transform.SetParent(transform, true);
        weapon = wep;
    }
    public override void clearItem() {
        weapon.transform.SetParent(null); // this fixes the scaling problem when setting player weapon.
        weapon = null;
        GameController.instance.playerUI.setPlayerWeapon(null); //astronomic hack
        enableBoxCollider();
    }
    public override void clearSkills() {
        GameController.instance.playerSkillManager.clearPlayerSkills();
    }
    public override Draggable getItemInSlot() {
        return weapon;
    }
}
