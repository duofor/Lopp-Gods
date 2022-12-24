using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : Action {
    
    public AttackAction( Sprite sprite ) {
        actionSprite = sprite;
    }

    public override void registerAction(Monster monster, EnemyUI enemyUI, Action action) {
        Debug.Log(monster.transform.name + "Plays an attack action");
        
        PlayerUI playerUI = GameController.instance.player.playerUI;
        GameController.instance.monsterController.enqueueAction(enemyUI, playerUI, action);

        //replenish the queue with the same action
        monster.addAction(this);
    }


}
