using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {
    Util util = new Util();

    [SerializeField] private List<Weapon> weapons = new List<Weapon>();

    public List<Weapon> getWeapons() {
        return weapons;
    }
}