using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {
    Util util = new Util();

    public Camera guiCamera;
    private Canvas battleCanvas;
    
    //pages
    [SerializeField] public CharacterInfo characterInfo; 
    [SerializeField] private RewardUI rewardUI;
    [SerializeField] private Page playerTurnPrompt;
    [SerializeField] private Page enemyTurnPrompt;
    [SerializeField] private Inventory inventoryBox;

    private bool hack = false;

    void Start() {
        GameObject battle = transform.Find("BattleCanvas").gameObject;
        battleCanvas = battle.GetComponent<Canvas>();
    }

    void Update() {
        showMenu();
    }


    void showMenu() {
    }

    public RewardUI getRewardUI() {
        return rewardUI;
    }

    public void displayPlayerTurnPrompt() {
        playerTurnPrompt.transform.position = new Vector3(0, 0, 0);
        GameController.instance.menuController.PushPage(playerTurnPrompt);
        StartCoroutine(sleepAndPopPage(util.turnPromptTimer, playerTurnPrompt));
    }

    public void displayEnemyTurnPrompt() {
        enemyTurnPrompt.transform.position = new Vector3(0, 0, 0);
        GameController.instance.menuController.PushPage(enemyTurnPrompt);
        StartCoroutine(sleepAndPopPage(util.turnPromptTimer,enemyTurnPrompt));
    }

    public Page getEnemyTurnPrompt() {
        return enemyTurnPrompt;
    }

    private IEnumerator sleepAndPopPage(float secondsToWait, Page page) {
        float timeElapsed = 0f;
        while (timeElapsed < secondsToWait) { // pause between attacks
            timeElapsed += Time.deltaTime;

            yield return null;
        }

        GameController.instance.menuController.PopPage();
        yield return null;
    }
}
