using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text livesText;
    public Text scoreText;

    void Start() {
        //brick destruction
    }

    private void onBrickDestruction() {
        scoreText.text = $@"SCORE:
        //create instance of game controller for score.
        ";
    }

}
