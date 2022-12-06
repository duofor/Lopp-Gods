using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action {

    public Sprite actionSprite {get; set;}

    public abstract void playAction(Monster monster, EnemyUI monsterUI, Action action);

}