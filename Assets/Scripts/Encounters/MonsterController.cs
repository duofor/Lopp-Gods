using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {
    Util util = new Util();

    public delegate void EndMonsterTurnEvent();
    public static event EndMonsterTurnEvent endMonsterTurnEvent;

    int oncePerRun = 1;
    
    private List<IEnumerator> attackAnimationQueue = new List<IEnumerator>();

    void Start() {
        GameMonsterActionState.startMonsterActions += playActions;
    }

    public void enqueueAction(EnemyUI monsterUI, PlayerUI playerUI, Action action) {
        // this shit needs to be refactored.
        if ( action.GetType() == typeof(AttackAction)) {
            attackAnimationQueue.Add(attackAnimation(monsterUI, playerUI, action));
        } else {
            attackAnimationQueue.Add(endTurnAfterSeconds(2));
        }
    }

    private void playActions() { 
        if ( attackAnimationQueue.Count <= 0 )
            return;
        oncePerRun = 0;

        IEnumerator nextAction = attackAnimationQueue[0];
        attackAnimationQueue.RemoveAt(0);
        StartCoroutine(nextAction);
    }

    IEnumerator attackAnimation(EnemyUI monsterUI, PlayerUI playerUI, Action action) {
        if (oncePerRun == 0 ) {
            oncePerRun = 1;
            float seconds = 1f; // just wait a little
            float timeElapsed = 0f;
            while ( timeElapsed < seconds ) {
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        } 

        if ( monsterUI.getEnemyObject() != null ) {
            float lerpDuration = 0.25f;
            float timeElapsed = 0f;

            Vector3 monsterUIInitialPosition = monsterUI.transform.position;

            float sizeX = monsterUI.GetComponent<SpriteRenderer>().bounds.size.x;
            Vector3 attackPosition = monsterUI.transform.position - new Vector3 (sizeX / 3, 0, 0);

            while (timeElapsed < lerpDuration) {
                monsterUI.transform.position = Vector3.Lerp(monsterUI.transform.position, attackPosition, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;

                if ( monsterUI.transform.position == attackPosition ) {
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
        }

        if ( attackAnimationQueue.Count > 0 ) {
            IEnumerator nextAction = attackAnimationQueue[0];
            attackAnimationQueue.RemoveAt(0);
            
            float timeElapsed = 0f;
            float timeToWait = 0.25f;
            while (timeElapsed < timeToWait) { // pause between attacks
                timeElapsed += Time.deltaTime;

                yield return null;
            }

            yield return StartCoroutine(nextAction); // start next action in queue.
        } else {
            StartCoroutine(endTurnAfterSeconds(util.monsterTransitionTimeSeconds));
        }
    }

    IEnumerator endTurnAfterSeconds(float secondsToWait) {
        Debug.Log($"ending monster turn after {secondsToWait} seconds");
        float timeElapsed = 0f;
        while (timeElapsed < secondsToWait) { // pause between attacks
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        endMonsterTurnEvent();
    }


}