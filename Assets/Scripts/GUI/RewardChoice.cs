using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardChoice : Page {

    private Card reward;
    private RewardUI rewardUI;

    Vector3 cardTempPosition = new Vector3( 600, 600, 0); 

    SpriteRenderer spriteRenderer;

    void Awake() {
        rewardUI = transform.parent.GetComponent<RewardUI>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void select() {
        if (rewardUI.isChoiceMade == true )
            return;

        Debug.Log("INSTANTIATING AND ADDING CARD");
        Card cardObj = Instantiate(reward, cardTempPosition, transform.rotation);
        cardObj.isUIOnly = false;
        GameController.instance.deck.addCardToDeck(cardObj);
        
        rewardUI.isChoiceMade = true;
    }

    public void setReward(Card card) {
        reward = card;
    }

    public void setRewardPosition() {
        reward.transform.position = transform.position;
    }

}