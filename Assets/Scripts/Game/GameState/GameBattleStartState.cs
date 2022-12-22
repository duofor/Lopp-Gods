using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameBattleStartState : GameBaseState {
    Util util = new Util();
    private Canvas battleCanvas;
    
    private int cardStartAmount = 4;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered Battle START state");
        battleCanvas = gameStateManager.battleGUI;
        enableGUI(); //enable when start GUI
        
        EnemyPositionController eps = GameController.instance.enemyPositionController;
        eps.arrangeEnemies();
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        gameStateManager.switchState(gameStateManager.battleState); // go to battling state
    }

    void enableGUI() {
        battleCanvas.enabled = true;
        Page page = util.getBattleGroundPage();
        GameController.instance.menuController.PushPage(page);
    }


}

