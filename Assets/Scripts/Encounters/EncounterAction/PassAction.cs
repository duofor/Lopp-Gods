using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAction : Action {

    public PassAction( Sprite sprite ) {
        actionSprite = sprite;
    }

    public override void registerAction(Monster monster, EnemyUI enemyUI, Action action) {
        Debug.Log("Played pass action. : did nothing");

        PlayerUI playerUI = GameController.instance.player.playerUI;
        GameController.instance.monsterController.enqueueAction(enemyUI, playerUI, action);
        
        //replenish queue with the same action.
        monster.addAction(this);

        return;
    }

}