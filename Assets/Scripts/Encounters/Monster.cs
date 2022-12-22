using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class Monster : MonoBehaviour {

    private int health = 2;
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

    void OnDestroy() {

    }

    IEnumerator doSomeSmallShakeAndDestroyObj( ) {
        Debug.Log("yeahh");
        Vector3 initialHitPosition = transform.position;

        float timePassed = 0;
        bool flip = false;
        while (timePassed < 0.3f) {
            // Shake
            if (flip) {
                flip = !flip; 
                transform.position += new Vector3(0, initialHitPosition.y / 80, 0);
                transform.position += new Vector3(initialHitPosition.x / 80, 0, 0);
            } else {
                flip = !flip; 
                transform.position -= new Vector3(0, initialHitPosition.y / 80, 0);
                transform.position -= new Vector3(initialHitPosition.x / 80, 0, 0);
            }
            timePassed += Time.deltaTime;
            yield return null;
        }

        transform.position = initialHitPosition;
        if (health <= 0) {
            canDestroy = true;
        }
    }

    public void takeDamage(int damage) {
        this.health -= damage;
        StartCoroutine(doSomeSmallShakeAndDestroyObj());
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
