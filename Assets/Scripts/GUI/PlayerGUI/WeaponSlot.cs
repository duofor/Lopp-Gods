using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : ItemSlot {
    Util util = new Util();

    private Weapon weapon;
    private SpriteRenderer weaponSpriteRenderer;

    List<Skill> instantiatedSkills;

    public override void setItem(Draggable wep) {
        weapon = (Weapon) wep;
        GameController.instance.playerUI.setPlayerWeapon(weapon); //astronomic hack
    }
    public override void clearItem() {
        weapon = null;
    }
    public override void clearSkills() {
        GameController.instance.playerSkillManager.clearPlayerSkills();
    }
    public override Draggable getItemInSlot() {
        return weapon;
    }

}
