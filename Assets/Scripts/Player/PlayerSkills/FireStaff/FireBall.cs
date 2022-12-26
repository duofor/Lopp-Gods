using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Skill {
    Util util = new Util();

    void Start() {
        numberOfTargets = 1;
        skillDamage = 1;
        skillManaCost = 2;
    }
    
    public override IEnumerator startAttackAnimation(GameObject target) {
        Vector3 targetPosition = target.transform.position;
        Vector3 skillStartingPosition = GameObject.Find("WeaponPoint").transform.position;

        GameObject skill = Instantiate( getSkillAnimationPrefab(), skillStartingPosition, transform.rotation );

        float timeElapsed = 0f;
        float lerpDuration = 5.5f;
        while ( timeElapsed < lerpDuration ) {
            skill.transform.position = Vector3.Lerp( skill.transform.position, targetPosition, timeElapsed / lerpDuration );
            timeElapsed += Time.deltaTime;

            if ( skill.transform.position == targetPosition ) {
                Destroy(skill.gameObject);
                fireAtTarget( target );
                yield break;
            }            
            yield return null;
        }
    }

}