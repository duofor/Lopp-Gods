using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState {
    
    public abstract void enterState(GameStateManager g);
    
    public abstract void updateState(GameStateManager g);

}

