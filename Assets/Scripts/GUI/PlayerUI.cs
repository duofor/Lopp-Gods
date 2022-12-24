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
    
    
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    // Outline material
    Material initialMaterial;
    private Material outlineMaterial;

    //hp
    GameObject healthBarPosition;
    public int maxHealth;
    int previousHealth;
    int health;
    
    // weapon
    [SerializeField] private Weapon weapon;
    private Vector3 weaponPosition;

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

        foreach ( Transform trans in transform ) {
            if (trans.name == "Weapon") {
                weaponPosition = trans.position;
                break;
            }
        }
    }

    void Update() {
        if ( maxHealth == 0 ) {
           maxHealth = GameController.instance.player.getMaxHealth();
        }

        bool fightState = GameController.instance.player.isInBattle;
        if ( fightState == false ) {
            spriteRenderer.enabled = false;
            healthSlider.transform.position = new Vector3 (9999, 9999, 0);
        } else {
            spriteRenderer.enabled = true;
            spriteRenderer.sortingOrder = 3;
            updateHealthBar();
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

    public void setWeapon(Weapon wep) {

        wep = Instantiate(wep);
        // wep.transform.position = weaponPosition; // Copies the Position 
        wep.transform.rotation = weapon.transform.rotation; // Copies the Rotation
        wep.transform.localScale = weapon.transform.localScale; // Copies the Scale
        Destroy(weapon.gameObject);
        weapon = wep;
        weapon.transform.position = transform.position;
        weapon.GetComponent<SpriteRenderer>().sortingOrder = 3; 
        weapon.transform.position = new Vector3( transform.position.x + 0.32f, transform.position.y + 0.16f, 0 );
        weapon.transform.parent = transform;
        weapon.transform.localScale = weapon.transform.localScale * 2; 

        // weapon = wep;
        // weapon.transform.position = weaponPosition;
        // weapon.transform.localScale = new Vector3(160, 160, 0);
    }
}
