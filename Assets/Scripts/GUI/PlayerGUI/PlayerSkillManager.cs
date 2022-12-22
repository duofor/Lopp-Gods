using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {


    //will keep this public for testing purposes
    public List<Skill> availableSkills; 
    public List<PlayerSkillSlot> playerSkillSlots; 


    void Start() {
        addTestSkill();
    }

    void Update() {

    }

    private void addTestSkill() {
        int index = 0;
        foreach ( Skill skill in availableSkills ) {
            Sprite skillSprite = skill.GetComponent<SpriteRenderer>().sprite;
            playerSkillSlots[index].setSkillSprite(skillSprite);
            playerSkillSlots[index].setOrderInLayer(3);
            playerSkillSlots[index].setSkill(skill);
            index += 1;
        }
    }

    public void endPlayerTurn() { // astronomic hack
        //this should be illegal
        GameController.instance.player.canEndTurn = true;
    }


}