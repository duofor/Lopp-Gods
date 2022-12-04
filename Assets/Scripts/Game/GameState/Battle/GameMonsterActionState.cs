using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonsterActionState : GameBaseState {
    Util util = new Util();

    List<EnemyUI> monstersUIInScene = new List<EnemyUI>();

    public override void enterState(GameStateManager gameStateManager) {
        Debug.Log("Entered GameMonsterActionState");
    
        //get monsters in scene
        monstersUIInScene = util.getMonstersInScene();
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        //needs to implement some functionality that tells if player died or sth
        foreach ( EnemyUI monsterUI in monstersUIInScene ) {
            Monster monster = monsterUI.getEnemyObject();
            if (monster == null) {
                continue;
            }
            Debug.Log("playing: " + monster);
            Action action = monster.getCurrentAction();
            action.playAction(monster);
        }
        
        // go to tranzition again
        gameStateManager.switchState( gameStateManager.playerActionState );
            
    }

}

