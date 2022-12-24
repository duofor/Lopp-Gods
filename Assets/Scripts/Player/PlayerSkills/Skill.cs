using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {
    Util util = new Util();

    public bool isSkillSelected = false;

    public SpriteRenderer spriteRenderer;
    Vector3 startMousePos;

    //test data
    public int damage = 1;
    public int skillManaCost = 1;

    void Awake() {
        isSkillSelected = false;
    }

    public IEnumerator loadSkill() {
        float timeElapsed = 0f;
        while ( timeElapsed < 0.1f ) { // wait a bit so that the click does not overlap with the fireAtTarget click check
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        isSkillSelected = true;
    }

    public void fireAtTarget() {
        int playerMana = GameController.instance.player.getMana();
        if ( playerMana < skillManaCost ) {
            Debug.Log("Insuficient mana to use skill");
            isSkillSelected = false;
            return;
        }

        Debug.Log("shits released");

        RaycastHit2D hit = util.getTargetAtMouse();
        if (hit && hit.collider.tag == util.enemyUITag ) {

            if ( hit.transform.gameObject == null ) {
                Debug.LogError(hit + "  is null ");
            } else {
                Monster monster = hit.transform.GetComponent<EnemyUI>().getEnemyObject();
                if ( monster != null ) {
                    //we are hitting an enemy
                    // reduce mana
                    playerMana -= skillManaCost;
                    GameController.instance.player.setMana(playerMana);

                    // send the damage
                    monster.takeDamage(damage);
                }
            }
        }
        isSkillSelected = false;
    } 
}