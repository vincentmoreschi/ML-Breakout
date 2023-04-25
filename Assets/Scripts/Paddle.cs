using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Vector3 paddlePositionInitial;

    // Start is called before the first frame update
    void Start()
    {
        paddlePositionInitial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Update paddle X position based on mouse X position
        Vector2 position;
        position.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        position.y = paddlePositionInitial.y;
        transform.position = position;
    }
}
