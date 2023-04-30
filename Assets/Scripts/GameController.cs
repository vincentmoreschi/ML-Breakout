using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string singlePlayerMode = "singleLevel1";
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
    // Not sure if this is needed for later.
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
