using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDeath;

    public Player player { get; set; }

    public float minY = -5 ;
    public float maxVelocity = 10f;

    private GameObject ballSpawn;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballSpawn) {
            if(transform.position.y < minY){
                transform.position = ballSpawn.transform.position;
                rb.velocity = Vector3.zero;
            }
            if(rb.velocity.magnitude > maxVelocity){
                rb.velocity = Vector3.ClampMagnitude(rb.velocity,maxVelocity);
            }
        }
    }

    public void Init(Player owner)
    {
        player = owner;
        transform.SetParent(player.transform);
    }

    public void Death() {
        OnBallDeath?.Invoke(this);
    }
}
