
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleAgent : Agent
{
    private Player player;

    // Start is called before the first frame update
    public int bricks; 
    public GameObject ball;
    
    private BallManager ballManager;

    void Start()
    {
        player = GameManager.Instance.players[1];  // AI player

        ball = GameObject.Find("Ball Red(Clone)");
       
       bricks = player.RemainingBricks;
    }

    public override void OnEpisodeBegin(){
        // GameManager.Instance.gameStarted = true;
        if(ball == null){
         ball = GameObject.Find("Ball Red(Clone)");
        }
        bricks = player.RemainingBricks;
        if(ball != null){
            if(ball.transform.position.y < -5){
                Debug.Log("dead");
                ball.transform.position = Vector3.zero;
                ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                GameManager.Instance.RestartGame();
            }
        }

    }
    public override void CollectObservations(VectorSensor sensor)
    {
        ball = GameObject.Find("Ball Red(Clone)");
        sensor.AddObservation(ball.transform.position);
        sensor.AddObservation(gameObject.transform.position);


    }
    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 controlSignal = new Vector3(actionBuffers.ContinuousActions[0], 0, 0);
       
        gameObject.transform.Translate(1  * controlSignal);

        if(player.RemainingBricks < bricks){
            SetReward(1.0f);
            Debug.Log("reward");
        }
        
        if (ball.transform.position.y < -4.5)
        {
            SetReward(-1.0f);
            Debug.Log("end");
            EndEpisode();
        }
        bricks = player.RemainingBricks;
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {

        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
    }
    
}
