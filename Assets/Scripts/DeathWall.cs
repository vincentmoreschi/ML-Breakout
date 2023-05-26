using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Ball") {
            Ball ball = collision.GetComponent<Ball>();
            ball.Death();
        }
    }
}
