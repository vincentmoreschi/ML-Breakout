using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            Ball ball = collision.GetComponent<Ball>();
            //TODO Remove ball method
        }
    }
}
