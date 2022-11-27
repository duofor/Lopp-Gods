using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {
    
    public Camera guiCamera;
    private Canvas canvasObject; // Assign in inspector
    private Canvas battleCanvas;

    void Start() {
        //camera init. temporarely using from inspector
        // GameObject objectWithTheCamera = transform.Find("GUICamera").gameObject;
        // guiCamera = objectWithTheCamera.GetComponent<Camera>();
        
        //canvas init
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
}
