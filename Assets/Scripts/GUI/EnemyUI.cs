using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour {
    Util util = new Util();

    public delegate void EnemyTakesDamageEvent(Transform transform);
    public static event EnemyTakesDamageEvent enemyTakesDamageEvent;

    [SerializeField] private Monster enemy;
    [SerializeField] private SpriteRenderer actionSprite;
    
    // Outline material
    private Material outlineMaterial;
  

    public int previousHealth; // used for dmg Testing 
    public int health; 
    private int fullHealth; 

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    Material initialMaterial;

    //health bar
    public Slider healthSlider;
    public Color healthColor;
    private GameObject healthBarPosition;

    //action
    
    //spawn positions
    Dictionary<GameObject, Vector3> spawnPositions;


    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = gameObject.AddComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;

        healthColor = Color.red;

        spriteRenderer.sortingOrder = 3;
        healthBarPosition = GameObject.Find("EnemyPositionController"); 

        foreach( Transform child in transform ) { // this will break if we have multiple childs of this transform
            actionSprite = child.GetComponent<SpriteRenderer>();
        }

        outlineMaterial = Resources.Load<Material>("Material/Outline_Material");
    }

    void Update() {
        
        if (enemy == null || spriteRenderer == null) {
            spriteRenderer.enabled = false;
        }
        
        if ( health > 0 )
            updateGUI();

        updateHealthBar();
        
        if (enemy == null) {
            displayActionSprite(); //disable sprite when no enemy
        }
    }

    void OnMouseOver() {
        if ( enemy != null ) {
            spriteRenderer.material = outlineMaterial;
        }
    }

    void OnMouseExit() {
        if ( initialMaterial != null ) {
            spriteRenderer.material = initialMaterial;
        }
    }

    void init() {
        spriteRenderer.sprite = enemy.GetComponent<SpriteRenderer>().sprite;
        transform.localScale = util.defaultGUIActorsScale;
    
        spriteRenderer.enabled = true;
        boxCollider.size = spriteRenderer.size;
        
        health = enemy.GetComponent<Monster>().getHealth();
        previousHealth = health; //we add 1 so tat we trigger first update health bar -> and show it in the ui
    } 

    public Monster getEnemyObject() {
        return enemy;
    }

    private void updateGUI() {
        health = enemy.GetComponent<Monster>().getHealth();
    }

    public int getHealth() {
        return health;
    }

    public void setEnemyObj( Monster enemyObj ) {
        if (enemyObj == null) {
            return;
        }
        Debug.Log("trying to set " + enemyObj.transform.name);
        enemy = enemyObj;
        init(); //all the components get init after setting the obj
        health = enemyObj.getHealth();

        //trash hp bar
        healthSlider.transform.localScale = new Vector3(spriteRenderer.size.x + spriteRenderer.size.x * 2.4f, spriteRenderer.size.y + spriteRenderer.size.y * 2.4f , 0);
        healthSlider.value = health;
        fullHealth = health;
        initialMaterial = enemy.GetComponent<SpriteRenderer>().material;
    }

    private void updateHealthBar() {
        float offset = 0.5f;
        healthSlider.transform.position = new Vector3 (transform.position.x, healthBarPosition.transform.position.y - offset, 0);
        if ( health > 0 && fullHealth > 0 ) {
            if (previousHealth != health) {
                StartCoroutine(doSomeSmallShake());
                enemyTakesDamageEvent(transform); // display dmg numbers
                previousHealth = health;
            }
            
            float value = health * 100 / fullHealth;
            healthSlider.value = value / 100;
            
        } else {
            if ( enemy != null ) {
                enemy.canDestroy = true;
            }
            healthSlider.value = 0;
        }
    }

    private Monster getEnemyObj() {
        return enemy;
    }

    public void displayActionSprite() {
        if ( enemy != null ) {
            actionSprite.sprite = enemy.peekNextAction().actionSprite;
            actionSprite.transform.position = new Vector3 (transform.position.x, transform.position.y + 1f, 0);
        } else {
            if ( actionSprite.sprite != null ) {
                actionSprite.sprite = null;
            }
        }
    }

    IEnumerator doSomeSmallShake( ) {
        Debug.Log("yeahh");
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
}
