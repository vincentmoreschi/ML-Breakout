using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;

public class PaddleAgentKC : Agent
{
    private Player player;

    private float _speed;
    private GameObject _background;
    private Vector3 _screenBounds;
    void Start()
    {
        Brick.OnBrickDestruction += OnBrickDestructionReward;
        Ball.OnBallDeath += OnBallDeathReward;

        player = GameManager.Instance.players[1];  // AI player

        _speed = player.paddle.speed;
        _background = GameObject.Find("Background");
        _screenBounds = _background.GetComponent<SpriteRenderer>().bounds.extents;
    }

    public override void OnEpisodeBegin()
    {
        LevelManager.Instance.ResetLevels(player);
        GameManager.Instance.ResetScore(player);
        GameManager.Instance.ResetLives(player);
        player.paddle.ResetPosition();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Paddle position
        sensor.AddObservation(transform.position);

        // Wall positions
        sensor.AddObservation(-1 * _screenBounds.x);
        sensor.AddObservation(_screenBounds.x);

        // Ball positions: Ray Perception Sensor

        // Brick positions:  Ray Perception Sensor
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Update paddle X position
        float moveX = actions.ContinuousActions[0];
        transform.Translate(_speed * Time.deltaTime * new Vector3(moveX, 0, 0));

        // Shoot the ball
        int shoot = actions.DiscreteActions[0];
        if (!player.gameStarted && shoot == 1)
        {
            player.gameStarted = true;
            BallManager.Instance.ShootBall(player);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");

        ActionSegment<int> discreteActions = actionsOut.DiscreteActions;
        discreteActions[0] = Input.GetKeyDown(KeyCode.Space) ? 1 : 0;
    }

    private void OnBrickDestructionReward(Brick brick)
    {
        SetReward(0.1f);
    }

    private void OnBallDeathReward(Ball obj)
    {
        SetReward(-1f);
        EndEpisode();
    }
}
