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
    }

    public void agentDifficulty (int difficulty) {
        Debug.Log("difficulty updated to " + difficulty);
        switch(difficulty)
        {
            case 0:
                GameSettings.agentDifficulty = 0;
                break;
            case 1:
                GameSettings.agentDifficulty = 1;
                break;
            case 2:
                GameSettings.agentDifficulty = 2;
                break;
        }
    }

    public void toggleColor () {
        if (GameSettings.ballColor == "Blue") {
            GameSettings.ballColor = "Red";    
        } else {
            GameSettings.ballColor = "Blue";    
        }
        // Debug.Log(GameSettings.ballColor);
    }
}