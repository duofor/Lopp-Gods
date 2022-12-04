using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour {

    
    [SerializeField] private Sprite attackActionSprite;
    [SerializeField] private Sprite passActionSprite;

    [System.NonSerialized]
    public AttackAction attackAction;
    public PassAction passAction;

    void Start() {
        attackAction = new AttackAction(attackActionSprite);
        passAction = new PassAction(passActionSprite);
    }
}