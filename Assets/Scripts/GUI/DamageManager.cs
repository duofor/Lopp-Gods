using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageManager : MonoBehaviour {
    
    public TextMeshProUGUI damageText;

    void Start() {
        PlayerUI.playerTakesDamageEvent += showDamageOnScreen;
        EnemyUI.enemyTakesDamageEvent += showDamageOnScreen;
    }

    // Update is called once per frame
    void Update() {

    }

    void showDamageOnScreen(Transform objTransform) {
        StartCoroutine( showDamageFor(objTransform) );
    }

    IEnumerator showDamageFor(Transform objTransform) {
        TextMeshProUGUI damageTextObj = Instantiate(damageText, objTransform.position, transform.rotation);
        
        damageTextObj.transform.SetParent(transform);
        damageTextObj.transform.localScale = new Vector3 (1,1,0);
        damageTextObj.text = "1";

        Vector3 objSize = objTransform.GetComponent<SpriteRenderer>().bounds.size;

        float yDest = objTransform.position.y + objSize.y;
        Vector3 destPosition = new Vector3(damageTextObj.transform.position.x, yDest, 0);

        float timeElapsed = 0f;
        float duration = 0.5f;
        while ( timeElapsed < duration ) {
            damageTextObj.transform.position = Vector3.Lerp(
                damageTextObj.transform.position,
                destPosition,
                timeElapsed 
            );

            timeElapsed += Time.deltaTime; 
            if ( damageTextObj.transform.position == destPosition ) {
                Destroy(damageTextObj.gameObject);
                yield break;
            }
            yield return null;
        }
        Destroy(damageTextObj.gameObject);

    }

}
