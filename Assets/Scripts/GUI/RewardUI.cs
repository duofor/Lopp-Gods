using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardUI : MonoBehaviour {

    public bool isChoiceMade = false;
    private int rewardsAmount = 3;
    Vector3 cardTempPosition = new Vector3( 600, 600, 0); 


    public List<Card> allAvailableCards;

    void Start() {
        List<Card> cards = GameController.instance.deck.getAllAvailableCards();
        foreach(Card card in cards) {
            Card obj = Instantiate(card, cardTempPosition, transform.rotation);
            obj.isUIOnly = true;
            allAvailableCards.Add(obj);
        }
    }

    public void disableUI() {
        GetComponent<Image>().enabled = false;

        foreach (Card card in allAvailableCards) {
            card.transform.position = cardTempPosition;
            card.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }

        foreach (Transform child in transform) {
            Button button = child.GetComponent<Button>();
            if (button) {
                button.gameObject.SetActive(false);
            }
        }
    }

    public void enableUI() {
        GetComponent<Image>().enabled = true;

        foreach (Transform child in transform) {
            Button button = child.GetComponent<Button>();
            if (button) {
                button.gameObject.SetActive(true);
            }
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
            child.GetComponent<RewardChoice>().setReward(choices[count]); //set the reward
            count += 1;
        }
    }


}
