using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private List<Monster> monstersOnTile;

    void Start() {
        monstersOnTile = new List<Monster>();
    }

    public void addEncounter(Monster monster) {
        Monster instantiate = Instantiate(monster, transform.position, transform.rotation); 
        monstersOnTile.Add(instantiate);
    }

    public void endEncounter() {
        foreach (Monster monster in monstersOnTile) {
            monster.canDestroy = true; //go ahead and let it begone
        }
        monstersOnTile.Clear(); //this causes the screen flashing bug. if forgotten
    
    }

    public List<Monster> getEncounter(){
        return monstersOnTile;
    }

}