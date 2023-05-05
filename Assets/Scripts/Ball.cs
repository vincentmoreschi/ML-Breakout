using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    
    public float minY = -5 ;

    private GameObject ballSpawn;
    public float maxVelocity = 10f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ballSpawn = GameObject.Find("BallSpawn");
       transform.position = ballSpawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY){
            transform.position= ballSpawn.transform.position;
            rb.velocity = Vector3.zero;
        }
        if(rb.velocity.magnitude > maxVelocity){
            rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVelocity);
        }
    }
    // Method for ball death.
    // public static event Action<Ball> OnBallDeath;
    
    // public void Death() {
    //     OnBallDeath?.Invoke(this);
    //     Destroy(ballSpawn, 1);
    // }
}
