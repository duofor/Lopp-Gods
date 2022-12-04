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
        Card.cardUseEvent += attacked;
        canDestroy = false;
        addSomeActions();
    }

    void Update() {
        if (canDestroy) {
            Destroy(transform.gameObject);
        }
    }

    void OnDestroy() {
        Card.cardUseEvent -= attacked;
    }

    void attacked(Card card, RaycastHit2D hit, int damage) {
        //stupid logic to bypass event being called everywhere
        EnemyUI script = hit.transform.gameObject.GetComponent<EnemyUI>();
        if ( script.getEnemyObject().gameObject != transform.gameObject ){
            return;
        }
        
        takeDamage(damage);
        StartCoroutine( doSomeSmallShakeAndDestroyObj( hit ) ); // this also destroyes
    }

    IEnumerator doSomeSmallShakeAndDestroyObj( RaycastHit2D hit ) {
        Debug.Log("yeahh");
        Vector3 initialHitPosition = hit.transform.position;

        float timePassed = 0;
        bool flip = false;
        while (timePassed < 0.3f) {
            // Shake
            if (flip) {
                flip = !flip; 
                hit.transform.position += new Vector3(0, initialHitPosition.y / 80, 0);
                hit.transform.position += new Vector3(initialHitPosition.x / 80, 0, 0);
            } else {
                flip = !flip; 
                hit.transform.position -= new Vector3(0, initialHitPosition.y / 80, 0);
                hit.transform.position -= new Vector3(initialHitPosition.x / 80, 0, 0);
            }
            timePassed += Time.deltaTime;
            yield return null;
        }

        hit.transform.position = initialHitPosition;
        if (health <= 0) {
            canDestroy = true;
        }
    }

    void takeDamage(int damage) {
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

    public void doSomeDamage() { //does 1 dmaage to the player --- test method
        GameController.instance.player.takeDamage(1);
    }
}
