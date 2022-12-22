using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoopNumber : MonoBehaviour {
    
    private TextMeshProUGUI loopNumberText;
    
    int loopNumber;

    void Start() {
        loopNumber = GameController.instance.player.loopNumber;
        loopNumberText = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        //can be changed to be fired by an event.
        loopNumber = GameController.instance.player.loopNumber;
        loopNumberText.text = "Loop: " + loopNumber.ToString();
    }


}
