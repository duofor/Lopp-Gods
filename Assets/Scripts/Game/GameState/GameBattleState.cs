using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();

    private List<GameObject> enemyObjs;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
        enemyObjs = util.getAllObjectsWithTag("EnemyUI");

    }

    public override void updateState(GameStateManager gameStateManager) {
        //end of battle trigger this >>
        if ( getTotalMonstersHealth() <= 0 ) {
            GameController.instance.player.finishBattle();
            
            gameStateManager.battleGUI.enabled = false; //disable gui when battle ends
            GameController.instance.deck.moveCardsFromHandToDeck();
            GameController.instance.deck.moveCardsFromUsedDeckToPrimaryDeck();

            GameController.instance.player.isInBattle = false;
            GameController.instance.player.getNextFloorToMove().endEncounter();

            gameStateManager.switchState( gameStateManager.loopState ); //go back to loop state
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

