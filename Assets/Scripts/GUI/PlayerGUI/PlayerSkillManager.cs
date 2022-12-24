using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour {


    //will keep this public for testing purposes
    public List<PlayerSkillSlot> playerSkillSlots; 

    public void addSkills( List<Skill> skills ) {
        int index = 0;

        foreach ( PlayerSkillSlot pss in playerSkillSlots ) {
            pss.clearSkill();
            pss.clearSkillSprite();
        }

        foreach ( Skill skill in skills ) {
            Sprite skillSprite = skill.GetComponent<SpriteRenderer>().sprite;
            playerSkillSlots[index].setSkillSprite(skillSprite);
            // playerSkillSlots[index].setOrderInLayer(3);
            playerSkillSlots[index].setSkill(skill);
            index += 1;
        }
    }

    public void endPlayerTurn() { // astronomic hack
        //this should be illegal
        GameController.instance.player.canEndTurn = true;
    }


}