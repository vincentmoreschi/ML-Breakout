using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static BallManager _instance;
    public static BallManager Instance => BallManager._instance;

    public Ball ballRedPrefab;
    public float ballStartForce;
    public float padding;  // Padding between ball and paddle

    private Ball _ball;
    private Rigidbody2D _ballRb;

    private void Awake()
    {
        if (BallManager._instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            BallManager._instance = this;
        }
    }
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

                _ballRb.AddForce(new Vector2(0, ballStartForce));
            }
        }
    }

    private void CreateBall(Ball ballPrefab)
    {
        Vector3 paddlePosition = Paddle.Instance.transform.position;
        Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + padding, paddlePosition.z);

        _ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity) as Ball;
        _ballRb = _ball.GetComponent<Rigidbody2D>();
    }

    internal void ResetBall()
    {
        Destroy(_ball.gameObject);
        CreateBall(ballRedPrefab);
    }

}
