using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBattleRewardState : GameBaseState {

    private RewardUI rewardUI;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered reward state");
        rewardUI = GameController.instance.guiController.getRewardUI();
        
        //display selection UI
        rewardUI.enableUI();
        rewardUI.init();

    }

    public override void updateState(GameStateManager gameStateManager) {
        
        if ( rewardUI.isChoiceMade == true ) {
            GameController.instance.player.finishBattle();
            
            gameStateManager.battleGUI.enabled = false; //disable gui when battle ends
            GameController.instance.deck.moveCardsFromHandToDeck();
            GameController.instance.deck.moveCardsFromUsedDeckToPrimaryDeck();

            GameController.instance.player.isInBattle = false;
            GameController.instance.player.getNextFloorToMove().endEncounter(); // getting current floor and ending the encounter
            rewardUI.disableUI();

            gameStateManager.switchState( gameStateManager.loopState );
        } else {
        
        }
    }

}

