using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : Page {

    public bool isChoiceMade = false;
    private int rewardsAmount = 3;
    Vector3 cardTempPosition = new Vector3( 9999, 9999, 0); 


    public List<Card> allAvailableCards;

    void Start() {
        List<Card> cards = GameController.instance.deck.getAllAvailableCards();
        foreach(Card card in cards) {
            Card obj = Instantiate(card, cardTempPosition, transform.rotation);
            obj.isUIOnly = true;
            allAvailableCards.Add(obj);
        }
    }

    public void removeChoiceCardsFromScreen() {
        foreach (Card card in allAvailableCards) {
            card.transform.position = cardTempPosition;
        }
    }

    public void init() {
        isChoiceMade = false;

        List<Card> choices = new List<Card>();
        for (int i = 0; i < rewardsAmount; i++) {
            int random = Random.Range(0, allAvailableCards.Count);
            choices.Add(allAvailableCards[random]);
            allAvailableCards.Remove(allAvailableCards[random]); //remove so we dont pick the same card twice
        }

        foreach ( Card card in choices) {
            allAvailableCards.Add(card);// repop with the removed from above
        }

        int count = 0;
        foreach (Transform child in transform) {
            if (child.transform.tag == "Button")
                return;
            RewardChoice rewardChoice = child.GetComponent<RewardChoice>();
            rewardChoice.setReward(choices[count]); //set the reward
            rewardChoice.setRewardPosition(); // this can be later changed into setting the sprite position
            count += 1;
        }
    }


}
