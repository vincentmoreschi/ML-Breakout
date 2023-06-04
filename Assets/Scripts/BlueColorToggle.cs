using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlueColorToggle : MonoBehaviour
{
    Toggle b_toggle;

    void Start () {
        b_toggle = GetComponent<Toggle>();
    }

    public void toggleSelected() {
        if (b_toggle.isOn) {
            GameSettings.ballColor = "Blue";
        }
    }
}
