using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSword : Weapon {

    void Start() {
        List<Skill> skills = new List<Skill>();
        skills.Add( new SwordBasicAttackSkill() );
        skills.Add( new SwordSwingAttackSkill() );
        weaponSkills = skills;
    }
}