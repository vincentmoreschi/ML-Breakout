using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedColorToggle : MonoBehaviour
{
    Toggle r_toggle;

    void Start () {
        r_toggle = GetComponent<Toggle>();
    }

    public void toggleSelected() {
        if (r_toggle.isOn) {
            GameSettings.ballColor = "Red";
        }
    }
}
