using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorStorm : Skill {
    Util util = new Util();

    void Start() {
        numberOfTargets = 2;
        skillDamage = 2;
        skillManaCost = 1;
    }
    
    public override IEnumerator startAttackAnimation(GameObject target) {
        Vector3 targetPosition = target.transform.position;
        Vector3 skillStartingPosition = GameObject.Find("SkillsStartingPositions").transform.position;

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

        yield return null;
    }

}