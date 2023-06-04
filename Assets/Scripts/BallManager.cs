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
    public Ball ballBluePrefab;
    public float ballStartForce;
    public float padding;  // Padding between ball and paddle

    void Start()
    {
        foreach (Player player in GameManager.Instance.players)
        {
            if (player != null)
            {
                if (GameSettings.ballColor == "Red") {
                    CreateBall(player, ballRedPrefab);
                } else {
                    CreateBall(player, ballBluePrefab);
                }
            }
        }
    }

    private void LateUpdate()
    {
        foreach (Player player in GameManager.Instance.players)
        {
            if (player != null && !player.gameStarted && player.balls.Count == 1)
            {
                Ball ball = player.balls[0];

                Vector3 paddlePosition = player.paddle.transform.position;
                Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + padding, paddlePosition.z);
                ball.transform.position = ballPosition;

                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    player.gameStarted = true;
                    ShootBall(player);
                }
            }
        }
    }

    /// <summary>
    /// Shoot the ball.
    /// 
    /// This method should only be called after balls have been reset and the game has not started.
    /// </summary>
    /// <param name="player"></param>
    public void ShootBall(Player player)
    {
        if (player.balls.Count == 1)
        {
            Ball ball = player.balls[0];
            ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, ballStartForce));
        }
    }

    public void CreateBall(Player player, Ball ballPrefab)
    {
        Vector3 paddlePosition = player.paddle.transform.position;
        Vector3 ballPosition = new Vector3(paddlePosition.x, paddlePosition.y + padding, paddlePosition.z);

        Ball ball = Instantiate(ballPrefab, ballPosition, Quaternion.identity) as Ball;
        ball.Init(player);

        player.balls = new List<Ball> { ball };
    }

    public void DestroyBalls(Player player)
    {
        foreach (Ball ball in player.balls)
        {
            Destroy(ball.gameObject);
        }
    }

    public void ResetBall(Player player)
    {
        DestroyBalls(player);
        if (GameSettings.ballColor == "Red") {
            CreateBall(player, ballRedPrefab);
        } else {
            CreateBall(player, ballBluePrefab);
        }
    }
}
