using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : Page {

    public bool isChoiceMade = false;
    private int rewardsAmount = 3;
    Vector3 cardTempPosition = new Vector3( 9999, 9999, 0); 

    void Start() {
    }

    public void init() {
        isChoiceMade = false;
    }


}
