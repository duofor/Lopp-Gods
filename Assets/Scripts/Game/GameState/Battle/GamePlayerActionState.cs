using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerActionState : GameBaseState {
    
    int cardsUsedThisTurn; //for testing


    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GamePlayerActionState");
        cardsUsedThisTurn = 0;
        Card.cardUseEvent += usedCard; 
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        if ( cardsUsedThisTurn >= 2 ) {
            cardsUsedThisTurn = 0;
            gameStateManager.switchState(gameStateManager.gameBattleTransitionState);
        }


    }

    void usedCard(Card card, RaycastHit2D hit, int damage) {
        cardsUsedThisTurn += 1;
    }

}

