using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSlot : MonoBehaviour {
    Util util = new Util();

    private SpriteRenderer spriteRenderer;
    private Image image;
    private Skill skill; 

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    void Update() {
        if (skill.isSkillSelected == true) {
            if (Input.GetMouseButtonUp(0)) {
                skill.fireAtTarget();
            }
        }
    }

    void OnMouseUp() {
        Debug.Log("Selected");
        StartCoroutine(skill.loadSkill());
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