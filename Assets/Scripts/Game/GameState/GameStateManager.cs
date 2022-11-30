using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour {
    
    public GameBaseState currentState;
    public GameLoopState loopState = new GameLoopState();
    public GameBattleState battleState = new GameBattleState();
    public GameBattleStartState battleStartState = new GameBattleStartState();

    public Canvas battleGUI; 

    void Start() {
        currentState = loopState;
        currentState.enterState(this);
    }

    void Update() {
        currentState.updateState(this);
    }

    public void switchState(GameBaseState state) {
        this.currentState = state;
        this.currentState.enterState(this);
    }

    public GameBaseState getState() {
        return currentState;
    }

}

