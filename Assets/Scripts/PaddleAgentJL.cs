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
    private int currentLevel;

    private Player player;

    // private Ball target;

    private Brick[] bricksList;

    void Start()
    {
        player = GameManager.Instance.players[1]; 
        bricks = player.RemainingBricks;
        brickContainer = player.bricksContainer;
        episodeCount = 0;
        leftWall.Set(-2.8f,-4.2f,0);
        rightWall.Set(2.8f,-4.2f,0);
    }
    public override void OnEpisodeBegin(){
        // GameManager.Instance.ResetLives(player);
        LevelManager.Instance.ResetLevels(player);
        GameManager.Instance.ResetLives(player);
        GameManager.Instance.ResetScore(player);
        player.paddle.ResetPosition();
        ball = GameObject.Find("Ball Red(Clone)");
        brickContainer = GameObject.Find("Bricks Container");
        bricksList = brickContainer.GetComponentsInChildren<Brick>();
        bricks = player.RemainingBricks;
        episodeCount++;
        currentLives = player.lives;
        currentLevel = player.currentLevel; 
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        if (ball != null) {
            sensor.AddObservation(Vector3.Normalize(ball.transform.localPosition));
            sensor.AddObservation(Vector3.Distance(ball.transform.localPosition,transform.localPosition));
            sensor.AddObservation(Vector3.Normalize(ball.transform.localPosition-transform.localPosition));
        }
        
        sensor.AddObservation(Vector3.Normalize(transform.localPosition));
        sensor.AddObservation(Vector3.Distance(leftWall,transform.localPosition));
        sensor.AddObservation(Vector3.Distance(rightWall,transform.localPosition));

        foreach (Brick brick in bricksList) {
            if (brick != null) {
                sensor.AddObservation(Vector3.Normalize(brick.transform.localPosition));
            }
        }

        Debug.Log(player.lives);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.ContinuousActions[0]);

        // if (ball==null) {
        //     SetReward(-0.5f);
        //     this.OnEpisodeBegin();
        // }

        if (player.lives < currentLives) {
          SetReward(-0.75f);
          currentLives = player.lives;
        }

        if (!player.gameStarted) {
            player.gameStarted = true;
            BallManager.Instance.ShootBall(player);
        }

        float moveX = actions.ContinuousActions[0];

        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

        SetReward(-0.001f);

        // if (ball != null && ball.transform.localPosition.y < -4.7f) {
        //     SetReward(-0.75f);
        //     player.lives -= 1;
        //     Debug.Log("reset lower than paddle");
        //     BallManager.Instance.ResetBall(player);
        //     BallManager.Instance.ShootBall(player);
        //     // EndEpisode();
        // }

        if (ball != null && ball.transform.localPosition.y > 5.0f) {
            Debug.Log("reset higher than wall");
            BallManager.Instance.ResetBall(player);
            // EndEpisode();
        }

        if (ball != null && (ball.transform.localPosition.x < -2.7f || ball.transform.localPosition.x > 2.7f))  {
            SetReward(0.001f);
        }

         if (player.RemainingBricks < bricks) {
            for (int i=0;i<bricks-(player.RemainingBricks);i++) {
                // Debug.Log("Brick break reward");
                SetReward(.5f);
            }
            bricks = player.RemainingBricks;
            
        }
        
        if (currentLevel<player.currentLevel) {
            SetReward(0.75f);
            currentLevel = player.currentLevel;
        }
        if (player.currentLevel == 11) {
            SetReward(1f);
            player.victoryScreen.SetActive(false);
            player.gameStarted = false;
            BallManager.Instance.CreateBall(player, BallManager.Instance.ballRedPrefab);
            EndEpisode();
        }
        if (player.gameOverScreen.activeSelf) {
            SetReward(-1f);
            player.gameOverScreen.SetActive(false);
            player.gameStarted = false;
            BallManager.Instance.CreateBall(player, BallManager.Instance.ballRedPrefab);
            EndEpisode();

        }

    }

    private void OnCollisionEnter2D(Collision2D other) {
      if (other.collider.tag.Equals("Ball")) {
        Debug.Log("ball hits paddle");
        SetReward(0.1f);
      }
    }


    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    }
}
