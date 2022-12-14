using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleRewardState : GameBaseState {

    private RewardUI rewardUI;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered reward state");
        rewardUI = GameController.instance.guiController.getRewardUI();
        
        //display selection UI
        GameController.instance.menuController.PushPage(rewardUI);
        rewardUI.isChoiceMade = false; //enabling player choice
    }

    public override void updateState(GameStateManager gameStateManager) {
        
        if ( rewardUI.isChoiceMade == true ) {
            GameController.instance.player.isInBattle = false;
            GameController.instance.player.getCurrentFloor().endEncounter(); // getting current floor and ending the encounter
            
            GameController.instance.menuController.PopPage(); // popping reward
            GameController.instance.menuController.PopPage(); // popping character info

            gameStateManager.switchState( gameStateManager.loopState );
        } else {
        
        }
    }

}

