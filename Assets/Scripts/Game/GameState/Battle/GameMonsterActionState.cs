using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonsterActionState : GameBaseState {
    Util util = new Util();

    public delegate void StartMonsterActions();
    public static event StartMonsterActions startMonsterActions;

    List<EnemyUI> monstersUIInScene = new List<EnemyUI>();

    int doOnce = 0;
    bool canSwitchState = false;

    public override void enterState(GameStateManager gameStateManager) {
        if (doOnce == 0 ) {
            MonsterController.endMonsterTurnEvent += monsterTurnEnded; //evant registering monster end turn
            doOnce = 1;
        }
        canSwitchState = false;
        
        Debug.Log("Entered GameMonsterActionState");
    
        //get monsters in scene
        monstersUIInScene = util.getMonstersInScene();
        foreach ( EnemyUI monsterUI in monstersUIInScene ) {
            Monster monster = monsterUI.getEnemyObject();
            if (monster == null) {
                continue;
            }
            Action action = monster.getCurrentAction();
            action.registerAction(monster, monsterUI, action);
        }

        startMonsterActions(); // triggers start attack for all mosnters on screen
    }
    
    public override void updateState(GameStateManager gameStateManager) {
        //needs to implement some functionality that tells if player died or sth

        //an event is fired when monster turn shall end        
        if (canSwitchState == true) {
            gameStateManager.switchState( gameStateManager.playerActionState );
        }
    }


    void monsterTurnEnded() {
        canSwitchState = true;
    }

}

