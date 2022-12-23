using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerActionState : GameBaseState {
    Util util = new Util();

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GamePlayerActionState");
        
        List<EnemyUI> monstersInScene = util.getMonstersInScene();
        foreach( EnemyUI enemyUI in monstersInScene ) {
            if ( enemyUI.getEnemyObject() != null ) {
                enemyUI.displayActionSprite(); // display the next action
            }
        }
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        if ( GameController.instance.player.canEndTurn == true || monstersDefeated() ) {
            gameStateManager.switchState(gameStateManager.gameBattleTransitionState);
            GameController.instance.player.canEndTurn = false;
        } 
    }

    private bool monstersDefeated() {
        List<EnemyUI> monsters = util.getMonstersInScene();
        foreach( EnemyUI enemy in monsters ) {
            if ( enemy.getHealth() > 0 ) {
                return false;
            }
        }

        return true;
    }
}

