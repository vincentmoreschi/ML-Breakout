using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    public static Paddle Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    public float speed;
    public float horizontalBounceMultiplier;  // Affects how much the ball bounces left or right during collisions

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector2 contactPoint = collision.GetContact(0).point;
        Vector2 paddleCenter = transform.position;

        ballRb.velocity = Vector2.zero;

        float diff = contactPoint.x - paddleCenter.x;
        float horizontalForce = diff * horizontalBounceMultiplier;  // Horizontal force applied to the ball, magnitude depends on contact point
        Vector2 force = new Vector2(horizontalForce, BallManager.Instance.ballStartForce);
        ballRb.AddForce(force);
    }

    public void ResetPosition()
    {
        transform.position = _paddlePositionInitial;
    }
}
