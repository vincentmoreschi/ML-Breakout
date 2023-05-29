using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgentJL : Agent
{
    // Start is called before the first frame update
    private int bricks; 
    private GameObject ball;
    private GameObject brickContainer;
    private GameObject walls;
    private int episodeCount;
    private Vector3 leftWall;
    private Vector3 rightWall;
    private int currentLives;

    private Player player;

    // private Ball target;

    private Brick[] bricksList;

    void Start()
    {
        // BallManager.Instance.CreateBall(target);
        bricks = player.RemainingBricks;
        brickContainer = player.bricksContainer;
        episodeCount = 0;
        leftWall.Set(-2.8f,-4.2f,0);
        rightWall.Set(2.8f,-4.2f,0);
    }
    // [SerializeField] private Transform ballTransform;
    public override void OnEpisodeBegin(){
        // BallManager.Instance.CreateBall(BallManager.Instance.ballRedPrefab);
        BallManager.Instance.ShootBall(player);
        player.gameStarted = true;
        ball = GameObject.Find("Ball Red(Clone)");
        brickContainer = GameObject.Find("Bricks Container");
        bricksList = brickContainer.GetComponentsInChildren<Brick>();
        bricks = player.RemainingBricks;
        walls = GameObject.Find("Walls");
        // Debug.Log(episodeCount);
        episodeCount++;
        // Debug.Log(ball);
        // Debug.Log(brickContainer);
        // Debug.Log(bricksList.GetLength(0));
        // Debug.Log(transform.position);
        // Debug.Log(ball.transform.position-transform.position);
        // Debug.Log(leftWall-transform.position);
        // Debug.Log(rightWall-transform.position);
        player.lives = 3;
        // currentLives = GameManager.Instance.lives;

    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // if (BallManager.Instance._ballRb.velocity.magnitude < 0.5f) {
        //     BallManager.Instance.ShootBall();
        // }

        // if (GameManager.Instance.lives < currentLives) {
        //     // Debug.Log("Deathwall Penalty");
        //     SetReward(-2.0f);
        //     BallManager.Instance.ShootBall();
        //     currentLives = GameManager.Instance.lives;
        // }
        // if (GameManager.Instance.lives == 1) {
        //     // Debug.Log("Deathwall Penalty");
        //     SetReward(-4.0f);
        //     EndEpisode();
        // }

        if (ball != null) {
            sensor.AddObservation(Vector3.Normalize(ball.transform.position));
            sensor.AddObservation(Vector3.Distance(ball.transform.position,transform.position));
        }
        
        sensor.AddObservation(Vector3.Normalize(transform.position));
        sensor.AddObservation(Vector3.Distance(leftWall,transform.position));
        sensor.AddObservation(Vector3.Distance(rightWall,transform.position));

        foreach (Brick brick in bricksList) {
            if (brick != null) {
                sensor.AddObservation(Vector3.Normalize(brick.transform.position));
            }
        }
        Debug.Log(bricksList.Length);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.ContinuousActions[0]);

        if (ball==null) {
            this.OnEpisodeBegin();
        }

        // if (ball != null && ball.GetComponent<Rigidbody2D>().velocity.magnitude < 1f) {
        //     BallManager.Instance.ShootBall();
        // }

        float moveX = actions.ContinuousActions[0];

        float moveSpeed = 10f;
        transform.position += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

        SetReward(-0.001f);

        if (ball != null && ball.transform.position.y < -4.7f) {
            SetReward(-0.5f);
            Debug.Log("reset lower than paddle");
            BallManager.Instance.ResetBall(player);
            // EndEpisode();
        }

        if (ball != null && ball.transform.position.y > 5.0f) {
            Debug.Log("reset higher than wall");
            BallManager.Instance.ResetBall(player);
            // EndEpisode();
        }

        if (ball != null && (ball.transform.position.x < -2.7f || ball.transform.position.x > 2.7f))  {
            SetReward(0.001f);
        }

         if (player.RemainingBricks < bricks) {
            for (int i=0;i<bricks-(player.RemainingBricks);i++) {
                // Debug.Log("Brick break reward");
                SetReward(.5f);
            }
            bricks = player.RemainingBricks;
            
        }
        if (player.RemainingBricks == 0) {
            SetReward(0.75f);
            EndEpisode();
        }
        if (player.currentLevel == 10) {
            SetReward(1f);
            player.victoryScreen.SetActive(false);
            GameManager.Instance.RestartGame();
            EndEpisode();
        }
        if (player.lives < 1) {
            SetReward(0.5f);
            player.gameOverScreen.SetActive(false);
            GameManager.Instance.RestartGame();
            EndEpisode();
        }
    }

    // private void OnCollisionEnter(Collision other) {
    //     if (other.gameObject.CompareTag("Ball")) {
    //         Debug.Log("paddle hit ball");
    //         SetReward(0.01f);
    //     }
    // }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    }
}
