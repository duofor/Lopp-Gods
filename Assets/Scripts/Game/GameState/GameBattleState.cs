using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();

    private GameObject enemyObj;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
        enemyObj = GameObject.Find("EnemyUI");
        

    }

    public override void updateState(GameStateManager gameStateManager) {
        // here goes the battle logic
        int enemyHealth = enemyObj.GetComponent<EnemyUI>().getHealth();

        //end of battle trigger this >>
        if ( enemyHealth <= 0 ) {
            GameController.instance.player.finishBattle();
            gameStateManager.battleGUI.enabled = false; //disable gui when battle ends
            gameStateManager.switchState( gameStateManager.loopState ); //go back to loop state
        }
    }

    private void disableGUI() {

    }

}

