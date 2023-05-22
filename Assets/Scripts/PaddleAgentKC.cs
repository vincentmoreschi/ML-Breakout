using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using System;

public class PaddleAgentKC : Agent
{
    private float _speed;
    private GameObject _background;
    private Vector3 _screenBounds;

    void Start()
    {
        Brick.OnBrickDestruction += OnBrickDestructionReward;
        Ball.OnBallDeath += OnBallDeathReward;

        _speed = gameObject.GetComponent<Paddle>().speed;
        _background = gameObject.GetComponent<ClampToBoundaries>().background;
        _screenBounds = _background.GetComponent<SpriteRenderer>().bounds.extents;
    }

    public override void OnEpisodeBegin()
    {
        //GameManager.Instance.RestartGame();

        GameManager.Instance.lives = 3;
        GameManager.Instance.score = 0;

        LevelManager.Instance.ResetLevels();

        UIManager.Instance.UpdateLevelText();
        UIManager.Instance.UpdateLivesText();
        UIManager.Instance.UpdateScoreText();

        transform.position = new Vector3(0, -4.2f, 0);
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
        if (!GameManager.Instance.gameStarted && shoot == 1)
        {
            GameManager.Instance.gameStarted = true;
            BallManager.Instance.ShootBall();
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
