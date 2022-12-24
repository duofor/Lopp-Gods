using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSlot : MonoBehaviour {
    Util util = new Util();

    private Image image;
    private Skill skill; 
    private Material outlineMaterial;
    private SpriteRenderer spriteRenderer;
    
    private Material initialMaterial;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        
        outlineMaterial = Resources.Load<Material>("Material/Outline_Material");
        initialMaterial = spriteRenderer.material;
    }

    void Update() {
        if (skill != null && skill.isSkillSelected == true) {
            if (Input.GetMouseButtonUp(0)) {
                skill.fireAtTarget();
            }
        }
    }

    void OnMouseUp() {
        Debug.Log("Selected");
        StartCoroutine(skill.loadSkill());
    }

    void OnMouseOver() {
        if ( transform.gameObject != null ) {
            image.material = outlineMaterial;
        }
    }

    void OnMouseExit() {
        if ( initialMaterial != null ) {
            image.material = initialMaterial;
        }
    }


    public void setSkillSprite ( Sprite sprite ) {
        image.sprite = sprite;
    }   

    public void setOrderInLayer( int number ) {
        // image = number;
    }

    public void setSkill(Skill skillToSet) {
        skill = skillToSet;
    }
}