using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBasicAttackSkill : Skill {
    Util util = new Util();

    void Start() {
        skillDamage = 1;
        skillManaCost = 1;
    }
    
}