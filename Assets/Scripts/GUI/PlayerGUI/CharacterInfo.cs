using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : Page {

    [SerializeField] private Slider manaSlider;
    public int maxMana;

    void Update() {
        if ( maxMana == 0 ) {
           maxMana = GameController.instance.player.getMaxMana();
        }
        updateManaBar();
    }


    private void updateManaBar() {
        int mana = GameController.instance.player.getMana();

        if ( mana > 0 ) {
            float value = mana * 100 / maxMana;
            manaSlider.value = value / 100;
            
        } else {
            manaSlider.value = 0;
        }
    }

}
