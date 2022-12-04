using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action {
    
    public AttackAction( Sprite sprite ) {
        actionSprite = sprite;
    }

    public override void playAction(Monster monster, EnemyUI enemyUI, Action action) {
        Debug.Log(monster.transform.name + "Plays an attack action");
        
        PlayerUI playerUI = GameController.instance.player.playerUI;
        

        monster.attack(enemyUI, playerUI, action); // does 1 dmg to the player. test method
        //replenish the queue with the same action
        monster.addAction(this);
    }


}
