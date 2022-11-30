using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopState : GameBaseState {

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered Loop state");
    }

    public override void updateState(GameStateManager gameStateManager) {
        if (GameController.instance.player.isInBattle) {
            //We start the battle -> moving the starting state
            gameStateManager.switchState(gameStateManager.battleStartState);
        }

        GameController.instance.player.loop();
    }

}

