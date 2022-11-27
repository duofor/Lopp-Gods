using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();

    private Canvas battleCanvas;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");
        battleCanvas = gameStateManager.battleGUI;
        enableOrDisable(); //enable when start GUI
    }

    public override void updateState(GameStateManager gameStateManager) {
        // here goes the battle logic
        GameController.instance.battle();
        //end of battle trigger this >>
        // enableOrDisable(); //disable when end GUI
        // gameStateManager.switchState( gameStateManager.loopState ); //go back to loop state
    }

    void enableOrDisable() {
        battleCanvas.enabled = !battleCanvas.enabled;
    }
}

