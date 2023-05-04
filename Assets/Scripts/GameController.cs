using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string singlePlayerMode = "Breakout";
    [SerializeField] private string versusMode = "versusLevel1";
    [SerializeField] private string optionsView = "optionsMenu";

    public void singlePlayerButton() {
        SceneManager.LoadScene(singlePlayerMode);
    }
    public void versusModeButton() {
        SceneManager.LoadScene(versusMode);
    }
    public void optionsButton() {
        SceneManager.LoadScene(optionsView);
    }

    public int lives{ get; set; }

    public int score{ get; set; }

    public int lifeCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.lives = this.lifeCount;
    }

    public void OnBallDeath(Ball obj) {
        //determine if ball is gone
        //reduce lives
        //if lives are < 1
        //show game over
        //else 
    }

    // // Update is called once per frame
    // void Update()
    // {

    // }
}
