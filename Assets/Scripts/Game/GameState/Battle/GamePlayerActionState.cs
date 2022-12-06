using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerActionState : GameBaseState {
    Util util = new Util();

    int cardsUsedThisTurn; //for testing


    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GamePlayerActionState");
        cardsUsedThisTurn = 0;
        Card.cardUseEvent += usedCard; 
        
        int cardAmount = 2;
        GameController.instance.deck.getRandomCardsFromDeck(cardAmount); //grab some cards
        GameController.instance.deck.setCardsInHandState(true);//enable cards in hand interactibity
        

        List<EnemyUI> monstersInScene = util.getMonstersInScene();
        foreach( EnemyUI enemyUI in monstersInScene ) {
            enemyUI.displayActionSprite();
        }
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        if ( cardsUsedThisTurn >= 2 ) {
            Card.cardUseEvent -= usedCard; 
            gameStateManager.switchState(gameStateManager.gameBattleTransitionState);
        }
    }

    void usedCard(Card card, RaycastHit2D hit, int damage) {
        cardsUsedThisTurn += 1;
    }

}

