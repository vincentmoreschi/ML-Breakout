using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed;

    private Vector3 _paddlePositionInitial;

    // Start is called before the first frame update
    void Start()
    {
        _paddlePositionInitial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Mouse X") != 0)  // If the mouse has moved along the X axis
        {
            // Update paddle X position based on mouse X position
            Vector2 position;
            position.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            position.y = _paddlePositionInitial.y;
            transform.position = position;
        }
        else
        {
            // Update paddle X position based on keyboard horizontal axis input and speed
            Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}
