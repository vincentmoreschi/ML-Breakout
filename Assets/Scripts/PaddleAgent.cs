using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgent : Agent
{
    // Start is called before the first frame update
    private int bricks; 
    private Ball ball;
    private GameObject brickContainer;

    void Start()
    {
        BallManager.Instance.CreateBall(ball);
        bricks = LevelManager.Instance.RemainingBricks;
        // ball = BallManager.Instance._ball;
        // brickContainer = LevelManager.Instance._bricksContainer;
    }
    // [SerializeField] private Transform ballTransform;
    public override void OnEpisodeBegin(){
        //GameManager.Instance.gameStarted = true;
        BallManager.Instance._ballRb.AddForce(new Vector2(0, BallManager.Instance.ballStartForce));
        GameManager.Instance.lives = 10;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        // sensor.AddObservation(ballTransform.position);
        // sensor.AddObservation(this.transform.position);
        // sensor.AddObservation(brickContainer.transform.localPosition);

    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        // Debug.Log(actions.ContinuousActions[0]);

        // float moveX = actions.ContinuousActions[0];

        // int moveSpeed = 7;
        // this.transform.position += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;

        // Vector3 controlSignal = Vector3.zero;

        // controlSignal.x = actionBuffers.ContinuousActions[0];

        // Vector3 direction = new Vector3(controlSignal.x, 0, 0);
        // this.transform.Translate(10  * controlSignal.x * direction);

        // if(LevelManager.Instance.RemainingBricks < bricks){
        //     SetReward(1.0f);
        // }
        // if (ball == null)
        // {
        //     EndEpisode();
        //     BallManager.Instance.ResetBall();
        // }
        // bricks = LevelManager.Instance.RemainingBricks
    }
private void OnCollisionEnter2D(Collision2D other) 
     {
        // if (other.otherCollider.TryGetComponent<Ball> (out Ball ball)) {
        //     SetReward(+1f);
        // }

        // if (other.otherCollider.TryGetComponent<Brick> (out Brick brick)) {
        //     SetReward(+2f);
        // }

        // if (other.otherCollider.TryGetComponent<DeathWall> (out DeathWall deathWall)) {
        //     SetReward(-2f);
        // }
        
        // if (GameManager.Instance.lives == 2) {
        //     EndEpisode();
        // }
    }

    // public override void Heuristic(in ActionBuffers actionsOut)
    // {
    //     ActionSegment<float> continuousActionsOut = actionsOut.ContinuousActions;
    //     continuousActionsOut[0] = Input.GetAxisRaw("Horizontal");
    // }
}
