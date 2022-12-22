using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChoice : MonoBehaviour {

    private RewardUI rewardUI;

    Vector3 cardTempPosition = new Vector3( 600, 600, 0); 

    void Awake() {
        rewardUI = transform.parent.GetComponent<RewardUI>();
    }

    public void select() {
        if (rewardUI.isChoiceMade == true )
            return;

        Debug.Log("INSTANTIATING AND ADDING CARD");
        rewardUI.isChoiceMade = true;
    }

    public void setReward() {
        // something
    }

    public void setRewardPosition() {
        // reward.transform.position = transform.position;
    }

}