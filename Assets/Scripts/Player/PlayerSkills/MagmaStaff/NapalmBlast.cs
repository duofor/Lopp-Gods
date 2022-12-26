using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NapalmBlast : Skill {
    Util util = new Util();

    void Start() {
        numberOfTargets = 1;
        skillDamage = 1;
        skillManaCost = 2;
    }
    
    // public override IEnumerator startAttackAnimation(GameObject target) {
    //     Vector3 targetPosition = target.transform.position;
    //     Vector3 skillStartingPosition = GameObject.Find("WeaponPoint").transform.position;

    //     GameObject skill = Instantiate( getSkillAnimationPrefab(), targetPosition, transform.rotation );

    //     float timeElapsed = 0f;
    //     float lerpDuration = 1.5f;
    //     while ( timeElapsed < lerpDuration ) {
    //         timeElapsed += Time.deltaTime;
    //         yield return null;
    //     }
        
    //     Destroy(skill.gameObject);
    //     fireAtTarget( target );
    // }


    public override IEnumerator startAttackAnimation(GameObject target) {
        Vector3 targetPosition = target.transform.position;
        Vector3 skillStartingPosition = GameObject.Find("WeaponPoint").transform.position;

        GameObject skill = Instantiate( getSkillAnimationPrefab(), skillStartingPosition, transform.rotation );

        float timeElapsed = 0f;
        float lerpDuration = 1.5f;
        while ( timeElapsed < lerpDuration ) {
            skill.transform.position = Vector3.Lerp( skill.transform.position, targetPosition, timeElapsed / lerpDuration );
            timeElapsed += Time.deltaTime;

            yield return null;
        }
        
        fireAtTarget( target );
        Destroy(skill.gameObject);
    }
}