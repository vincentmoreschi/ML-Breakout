using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScenario : MonoBehaviour
{
        private static GameOverScenario _instance;

        public static GameOverScenario Instance => GameOverScenario._instance;
        // private void OnDisable() {
            
        // }
        public void showGameOver() {
            gameObject.SetActive(true);
        }

        public void hideGameOver() {
            gameObject.SetActive(false);
        }
}   
