using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DeathWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.CompareTag("Ball")) {
            //Debug.Log("Collision with Deathwall");
            Ball ball = collision.GetComponent<Ball>();
            //BallManager.Instance.Balls.Remove(ball);
            ball.Death();
        }
    }
}
