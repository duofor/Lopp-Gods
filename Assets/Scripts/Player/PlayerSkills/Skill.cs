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
    public int cooldown = 1;

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
        Debug.Log("shits released");

        RaycastHit2D hit = util.getTargetAtMouse();
        if (hit && hit.collider.tag == util.enemyUITag ) {

            if ( hit.transform.gameObject == null ) {
                Debug.LogError(hit + "  is null ");
            } else {
                Monster monster = hit.transform.GetComponent<EnemyUI>().getEnemyObject();
                if ( monster != null ) {
                    // Debug.Log("Targeted: " + hit.transform.name.ToString());
                    monster.takeDamage(damage);
                }
            }
        }
        isSkillSelected = false;
    } 
}