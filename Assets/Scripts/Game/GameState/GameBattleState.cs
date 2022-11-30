using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleState : GameBaseState {
    Util util = new Util();


    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered battle state");

    }

    public override void updateState(GameStateManager gameStateManager) {
        // here goes the battle logic
        //end of battle trigger this >>
        // enableOrDisable(); //disable when end GUI
        // gameStateManager.switchState( gameStateManager.loopState ); //go back to loop state
    }

}

