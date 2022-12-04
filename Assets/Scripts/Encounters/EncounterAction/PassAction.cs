using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassAction : Action {

    public PassAction( Sprite sprite ) {
        actionSprite = sprite;
    }

    public override void playAction(Monster monster, EnemyUI monsterUI, Action action) {
        Debug.Log("Played pass action. : did nothing");
        return;
    }

}