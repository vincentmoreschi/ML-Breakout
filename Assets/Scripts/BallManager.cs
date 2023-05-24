using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    #region Singleton
    public static BallManager Instance { get; private set; }

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

    public Ball ballRedPrefab;
    public float ballStartForce;
    public float padding;  // Padding between ball and paddle

    public Ball _ball;
    public Rigidbody2D _ballRb;

    public List<Ball> Balls { get; set; }

    void Start()
    {
        CreateBall(ballRedPrefab);
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.gameStarted)
        {
            Vector3 paddlePosition = Paddle.Instance.transform.position;
            Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + padding, paddlePosition.z);
            _ball.transform.position = ballPosition;

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.gameStarted = true;
                ShootBall();
            }
        }
    }

    public void ShootBall()
    {
        _ballRb.AddForce(new Vector2(0, ballStartForce));
    }

    public void CreateBall(Ball ballPrefab)
    {
        Vector3 paddlePosition = Paddle.Instance.transform.position;
        Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + padding, paddlePosition.z);

        _ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity) as Ball;
        _ballRb = _ball.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball> {
            _ball
        };
    }

    public void DestroyBalls()
    {
        foreach (var ball in this.Balls)
        {
            Destroy(ball.gameObject);
        }
    }

    internal void ResetBall()
    {
        DestroyBalls();
        CreateBall(ballRedPrefab);
    }
}
