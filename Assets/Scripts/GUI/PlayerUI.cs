using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    Util util = new Util();


    public delegate void PlayerTakesDamageEvent(Transform transform);
    public static event PlayerTakesDamageEvent playerTakesDamageEvent;

    [SerializeField] private GameObject player;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider manaSlider;
    
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    // Outline material
    Material initialMaterial;
    private Material outlineMaterial;

    GameObject healthBarPosition;
    public int maxHealth;
    public int maxMana;
    
    int previousHealth;
    int health;

    int doOnce = 0;
    
    void Awake() {
        if (boxCollider == null ) {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }

        if (spriteRenderer == null) {
            spriteRenderer = GetComponent<SpriteRenderer>();

        }
        
        transform.localScale = util.defaultGUIActorsScale;
        spriteRenderer.sprite = player.GetComponent<SpriteRenderer>().sprite;
        boxCollider.size = spriteRenderer.size;
        healthBarPosition = GameObject.Find("EnemyPositionController"); 
        PlayerController.playerUIHitEffect += shake;
        doOnce = 0;

        outlineMaterial = Resources.Load<Material>("Material/Outline_Material");
        
        if ( initialMaterial == null ) {
            initialMaterial = GetComponent<SpriteRenderer>().material;
        }
    }

    void Update() {
        if ( maxHealth == 0 ) {
           maxHealth = GameController.instance.player.getMaxHealth();
        }

        if ( maxMana == 0 ) {
           maxMana = GameController.instance.player.getMaxMana();
        }

        bool fightState = GameController.instance.player.isInBattle;
        if ( fightState == false ) {
            spriteRenderer.enabled = false;
            healthSlider.transform.position = new Vector3 (9999, 9999, 0);
        } else {
            spriteRenderer.enabled = true;
            spriteRenderer.sortingOrder = 3;
            updateHealthBar();
            updateManaBar();
        }
    }

    void OnMouseOver() {
        if ( spriteRenderer != null ) {
            spriteRenderer.material = outlineMaterial;
        }
    }

    void OnMouseExit() {
        if ( initialMaterial != null ) {
            spriteRenderer.material = initialMaterial;
        }
    }


    private void updateHealthBar() {
        float offset = 0.5f;
        healthSlider.transform.position = new Vector3 (transform.position.x, healthBarPosition.transform.position.y - offset, 0);
        health = GameController.instance.player.getHealth();
        if (doOnce == 0) {
            previousHealth = health;
            doOnce = 1;
        }

        if ( health > 0 ) {
            if (previousHealth != health) {
                playerTakesDamageEvent(transform); // display dmg numbers
                previousHealth = health;
            }

            float value = health * 100 / maxHealth;
            healthSlider.value = value / 100;
            
        } else {
            healthSlider.value = 0;
        }
    }

    private void updateManaBar() {
        int mana = GameController.instance.player.getMana();

        if ( mana > 0 ) {
            float value = mana * 100 / maxMana;
            manaSlider.value = value / 100;
            
        } else {
            manaSlider.value = 0;
        }
    }

    IEnumerator doSomeSmallShake() {
        Debug.Log("ouch");
        Vector3 initialHitPosition = transform.position;

        float timePassed = 0;
        bool flip = false;
        while (timePassed < 0.3f) {
            // Shake
            if (flip) {
                flip = !flip; 
                transform.position += new Vector3(0, initialHitPosition.y / 80, 0);
                transform.position += new Vector3(initialHitPosition.x / 80, 0, 0);
            } else {
                flip = !flip; 
                transform.position -= new Vector3(0, initialHitPosition.y / 80, 0);
                transform.position -= new Vector3(initialHitPosition.x / 80, 0, 0);
            }
            timePassed += Time.deltaTime;
            yield return null;
        }

        transform.position = initialHitPosition;
    }

    void shake() {
        StartCoroutine(doSomeSmallShake());
    }
}
