using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleTransitionState : GameBaseState {
    Util util = new Util(); //for gods sake make a singleton

    private List<GameObject> enemyObjs;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GameBattleTransitionState");
        enemyObjs = util.getAllObjectsWithTag(util.enemyUITag);

        //disable cards in hand interactibity
        GameController.instance.deck.setCardsInHandState(false);
    }

    
    public override void updateState(GameStateManager gameStateManager) {
        if ( getTotalMonstersHealth() <= 0 ) {
            gameStateManager.switchState( gameStateManager.battleRewardState ); //go to reward state
        } else {
            
            // continue to monster action state
            gameStateManager.switchState( gameStateManager.monsterActionState ); //go to reward state


        }


    }

    private int getTotalMonstersHealth() {
        int mobHp = 0;
        foreach (GameObject enemyGo in enemyObjs ) {
            EnemyUI script = enemyGo.GetComponent<EnemyUI>();
            if ( script.enabled == true ) {
                mobHp += script.getHealth();
            }
        }
        return mobHp;
    }




}

