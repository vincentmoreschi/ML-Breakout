using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class PaddleBrainJL : Agent
{
    // Start is called before the first frame update
    private int bricks; 
    private GameObject ball;
    private GameObject brickContainer;
    private GameObject walls;
    private Vector3 leftWall;
    private Vector3 rightWall;


    private Player player;


    private Brick[] bricksList;

    void Start()
    {
        player = GameManager.Instance.players[1]; 
        bricks = player.RemainingBricks;
        brickContainer = player.bricksContainer;
        leftWall.Set(-2.8f,-4.2f,0);
        rightWall.Set(2.8f,-4.2f,0);
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        if (ball != null) {
            sensor.AddObservation(ball.transform.localPosition);
            sensor.AddObservation(Vector3.Distance(ball.transform.localPosition,this.transform.localPosition));
        }
        
        sensor.AddObservation(this.transform.localPosition);
        sensor.AddObservation(Vector3.Distance(leftWall,this.transform.localPosition));
        sensor.AddObservation(Vector3.Distance(rightWall,this.transform.localPosition));


    }
    public override void OnActionReceived(ActionBuffers actions)
    {

        if (!player.gameStarted) {
            player.gameStarted = true;
            BallManager.Instance.ShootBall(player);
        }

        int move = actions.DiscreteActions[0];
        float moveSpeed = 10f;
        float moveX = 0.75f;
        switch (move)
        {
            case 0:
                transform.localPosition += new Vector3(-moveX,0,0) * Time.deltaTime * moveSpeed;
                break;
            case 1:
                break;
            case 2:
                transform.localPosition += new Vector3(moveX,0,0) * Time.deltaTime * moveSpeed;
                break;
        }

    }

}
