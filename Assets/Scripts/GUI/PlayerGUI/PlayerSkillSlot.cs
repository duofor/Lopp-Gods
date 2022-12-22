using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSlot : MonoBehaviour {
    Util util = new Util();

    private SpriteRenderer spriteRenderer;
    private Skill skill; 

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (skill.isSkillSelected == true) {
            if (Input.GetMouseButtonUp(0)) {
                skill.fireAtTarget();
            }
        }
    }

    void OnMouseUp() {
        StartCoroutine(skill.loadSkill());
    }

    public void setSkillSprite ( Sprite sprite ) {
        spriteRenderer.sprite = sprite;
    }   

    public void setOrderInLayer( int number ) {
        spriteRenderer.sortingOrder = number;
    }

    public void setSkill(Skill skillToSet) {
        skill = skillToSet;
    }
}