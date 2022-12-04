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

    public void attack(EnemyUI monsterUI, PlayerUI playerUI, Action action) { //does 1 dmaage to the player --- test method
        StartCoroutine(attackAnimation(monsterUI, playerUI, action));
    }

    IEnumerator attackAnimation(EnemyUI monsterUI, PlayerUI playerUI, Action action) {
        while ( action.canStartAction == false ) {
            Debug.Log( "Waiting 1 more second" );
            yield return new WaitForSeconds(0.5f);
        }
        
        // we dont let next mob attack. Preventing all mobs attacking at the same time.
        action.canStartAction = false;

        float lerpDuration = 1.5f;
        float timeElapsed = 0f;

        Vector3 monsterUIInitialPosition = monsterUI.transform.position;

        float sizeX = monsterUI.GetComponent<SpriteRenderer>().bounds.size.x;
        Vector3 playerUIPosition = playerUI.transform.position - new Vector3 (-sizeX,0,0);

        while (timeElapsed < lerpDuration) {
            monsterUI.transform.position = Vector3.Lerp(monsterUI.transform.position, playerUIPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            if ( monsterUI.transform.position == playerUIPosition ) {
                GameController.instance.player.takeDamage(1); //do some dummy damage
                break;
            }
                
            yield return null;
        }


        timeElapsed = 0f;
        while (timeElapsed < lerpDuration) {
            monsterUI.transform.position = Vector3.Lerp(monsterUI.transform.position, monsterUIInitialPosition, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;

            if ( monsterUI.transform.position == monsterUIInitialPosition ) 
                break;
            yield return null;
        }

        //let the next mob start his attack now
        action.canStartAction = true;
    }
}
