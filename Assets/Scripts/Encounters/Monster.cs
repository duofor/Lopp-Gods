using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Monster : MonoBehaviour {

    private int health = 10;
    public bool canDestroy;

    Action currentAction;
    //this need to stay in the Type of monster class
    Queue<Action> actionQueue = new Queue<Action>();

    void Awake() {
        canDestroy = false;
        addSomeActions();
    }

    void Update() {
        if (canDestroy) {
            Destroy(transform.gameObject);
        }
    }

    public void takeDamage(int damage) {
        this.health -= damage;
    }

    public void setHealth( int healthAmount ) {
        this.health = healthAmount;
    }

    public int getHealth() {
        return health;
    }

    public void addSomeActions() { // for testing
        ActionManager am = GameController.instance.actionManager;

        actionQueue.Enqueue(am.attackAction);
        actionQueue.Enqueue(am.passAction);
        actionQueue.Enqueue(am.attackAction);
        actionQueue.Enqueue(am.passAction);
        actionQueue.Enqueue(am.attackAction);
        actionQueue.Enqueue(am.passAction);
        actionQueue.Enqueue(am.attackAction);
        actionQueue.Enqueue(am.passAction);
    }

    public Action getCurrentAction() {
        Action action = actionQueue.Dequeue();

        return action;
    }

    public Action peekNextAction() {
        Action action = actionQueue.Peek();

        return action;
    }

    public void addAction(Action action) {
        actionQueue.Enqueue(action);
    }
}
