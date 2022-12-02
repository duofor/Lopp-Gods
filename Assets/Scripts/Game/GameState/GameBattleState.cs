using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();

    private List<GameObject> enemyObjs;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
        enemyObjs = util.getAllObjectsWithTag(util.enemyUITag);

    }

    public override void updateState(GameStateManager gameStateManager) {
        //end of battle trigger this >>
        if ( getTotalMonstersHealth() <= 0 ) {
            gameStateManager.switchState( gameStateManager.battleRewardState ); //go to reward state
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

