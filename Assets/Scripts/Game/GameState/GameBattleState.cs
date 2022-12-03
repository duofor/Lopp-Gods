using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();

    private List<GameObject> enemyObjs;

    public bool canContinue = false;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
        enemyObjs = util.getAllObjectsWithTag(util.enemyUITag);
    }

    public override void updateState(GameStateManager gameStateManager) {
        gameStateManager.switchState( gameStateManager.playerActionState ); //go to reward state
    }

}

