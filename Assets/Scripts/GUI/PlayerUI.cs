using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    Util util = new Util();


    public delegate void PlayerTakesDamageEvent(Transform transform);
    public static event PlayerTakesDamageEvent playerTakesDamageEvent;

    [SerializeField] public GameObject player;
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
    [SerializeField] private GameObject weaponPoint;
    private Vector3 weaponPosition;
    private Weapon weapon;

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

        weaponPosition = weaponPoint.transform.position;
        transform.position = util.offscreenPosition; // init
    }

    void Update() {
        if ( maxHealth == 0 ) {
           maxHealth = GameController.instance.player.getMaxHealth();
        }
        Stack<Page> UIStack = GameController.instance.menuController.getStack();
        if ( UIStack.Count > 0 ) {
            Page lastUIElement = UIStack.Peek();
            if ( lastUIElement.GetType() == typeof(RewardUI) ) {
                transform.position = util.offscreenPosition;
                healthSlider.transform.position = util.offscreenPosition;
            } else {
                updateHealthBar();
            }
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

    public void setPlayerWeapon(Draggable wep) {
        if ( wep == null ) {// when we unequip we just destroy the current weapon in hand.
            Destroy(weapon.gameObject); 
            return;
        }
        
        if ( weapon != null ) {
            Destroy(weapon.gameObject);
        }

        Weapon weaponToSet = (Weapon) Instantiate(wep, util.defaultVector3, weaponPoint.transform.rotation);
        weaponToSet.GetComponent<SpriteRenderer>().sortingOrder = 3; 
        
        weaponToSet.transform.SetParent(transform); // so that when scene ends, wep moves out of screen
        weaponToSet.transform.position = weaponPoint.transform.position; 
        
        List<Skill> weaponSkills = weaponToSet.getWeaponSkills();
        GameController.instance.playerSkillManager.addSkills(weaponSkills);

        weapon = weaponToSet; // dunno why we still do this tho
    }

    public void setFightPlayerPosition() {
        GameObject playerPosCon = GameObject.Find(util.playerPositionController);
        transform.position = new Vector3( playerPosCon.transform.position.x * 2, playerPosCon.transform.position.y, 0 ); //center
    }
}
