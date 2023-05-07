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
    void Start()
    {
       BallManager.Instance.CreateBall(ball);
       bricks = LevelManager.Instance.RemainingBricks;
    }

    public override void OnEpisodeBegin(){
        GameManager.Instance.gameStarted = true;
        
        if(ball.transform.localPosition.y <= -4.8)
            BallManager.Instance.ResetBall();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(ball.transform.localPosition);
        sensor.AddObservation(this.transform.localPosition);


    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
        this.transform.Translate(10  * controlSignal.x * direction);
        if(LevelManager.Instance.RemainingBricks < bricks){
            SetReward(1.0f);
        }
        if (ball.transform.localPosition.y <= 4.8)
        {
            EndEpisode();
        }
        bricks = LevelManager.Instance.RemainingBricks;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }
}
