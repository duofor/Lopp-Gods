using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();


    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
    }

    public override void updateState(GameStateManager gameStateManager) {
        gameStateManager.switchState( gameStateManager.playerActionState ); //go to reward state
    }

}

