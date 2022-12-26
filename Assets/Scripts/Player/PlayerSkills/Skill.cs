using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill : MonoBehaviour {
    Util util = new Util();

    public bool isSkillSelected = false;

    private SpriteRenderer spriteRenderer;
    Vector3 startMousePos;

    [SerializeField] private GameObject skillAnimation_Prefab;

    //test data
    public int skillLevel = 1;
    public int skillDamage = 1;
    public int skillManaCost = 1;
    public int numberOfTargets = 1;

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

    public void fireAtTarget( GameObject hit ) {
        int playerMana = GameController.instance.player.getMana();
        if ( playerMana < skillManaCost ) {
            Debug.Log("Insuficient mana to use skill");
            isSkillSelected = false;
        }
        Debug.Log("shits released");

        Monster monster = hit.transform.GetComponent<EnemyUI>().getEnemyObject();
        if ( monster != null ) {
            playerMana -= skillManaCost;
            GameController.instance.player.setMana(playerMana);

            // send the damage
            monster.takeDamage(skillDamage);
        }
        isSkillSelected = false;
    } 

    public abstract IEnumerator startAttackAnimation(GameObject target);

    public GameObject getSkillAnimationPrefab() {
        return skillAnimation_Prefab;
    }
}