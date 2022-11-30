using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleStartState : GameBaseState {
    
    private Canvas battleCanvas;
    
    private int cardStartAmount = 4;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered Battle START state");
        battleCanvas = gameStateManager.battleGUI;
        enableGUI(); //enable when start GUI
        GameController.instance.initStartBattleCards( cardStartAmount ); // init player with cards first
    }
    
    public override void updateState(GameStateManager gameStateManager) {

    }

    void enableGUI() {
        battleCanvas.enabled = true;
    }


}

