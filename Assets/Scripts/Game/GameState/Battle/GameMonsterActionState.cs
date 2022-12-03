using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonsterActionState : GameBaseState {
    Util util = new Util();

    List<Monster> monstersInScene;

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GameMonsterActionState");
    
        //get monsters in scene
        List<GameObject> enemyGameObjects = util.getAllObjectsWithTag(util.enemyUITag);

        foreach ( GameObject go in enemyGameObjects ) {
            monstersInScene.Add( go.GetComponent<EnemyUI>().getEnemyObject() );
        }
    
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        //needs to implement some functionality that tells if player died or sth

        foreach ( Monster monster in monstersInScene ) {
            // Action action = monster.getCurrentAction();

        }


        GameController.instance.deck.setCardsInHandState(true);
        //enable cards in hand interactibity
        
        // go to tranzition again
        gameStateManager.switchState( gameStateManager.monsterActionState );
            
    }

}

