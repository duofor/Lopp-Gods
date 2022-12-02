using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {
    
    public Camera guiCamera;
    private Canvas canvasObject; // Assign in inspector
    private Canvas battleCanvas;

    [SerializeField] private RewardUI rewardUI;

    void Start() {
        GameObject objectWithTheCamera = transform.Find("Canvas").gameObject;
        canvasObject = objectWithTheCamera.GetComponent<Canvas>();

        GameObject battle = transform.Find("BattleCanvas").gameObject;
        battleCanvas = battle.GetComponent<Canvas>();
    }

    void Update() {
        showMenu();
    }


    void showMenu() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            canvasObject.enabled = !canvasObject.enabled;
        }
    }

    public RewardUI getRewardUI() {
        return rewardUI;
    }
}
