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
}
