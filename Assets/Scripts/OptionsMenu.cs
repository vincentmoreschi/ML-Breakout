using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class OptionsMenu : MonoBehaviour
{

    public void numberOfLives (int lives) {
        Debug.Log("lives updated to " + lives);
        switch(lives)
        {
            case 0:
                GameSettings.livesSelection = 3;
                break;
            case 1:
                GameSettings.livesSelection = 5;
                break;
            case 2:
                GameSettings.livesSelection = 10;
                break;
        }
        GameObject livesValue = GameObject.Find("Lives Dropdown");
        Debug.Log(livesValue);
    }

    // public void agentDifficulty (int difficulty) {
    //     Debug.Log("difficulty updated to " + difficulty);
    //     switch(difficulty)
    //     {
    //     }
    // }

    // public void finish () {
    //     TMPro.TMP_Dropdown livesDropdown;
    //     GameObject livesValue = GameObject.Find("Lives Dropdown");
    //     Debug.Log(livesValue);
    // }
}