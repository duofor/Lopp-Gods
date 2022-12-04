using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action {
    
    public AttackAction( Sprite sprite ) {
        actionSprite = sprite;
    }

    public override void playAction(Monster monster) {
        Debug.Log("Plays an aattack action");
        monster.doSomeDamage(); // does 1 dmg to the player. test method
        monster.addAction(this);
    }
}
