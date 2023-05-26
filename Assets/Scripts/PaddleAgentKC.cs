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
        player = GameManager.Instance.players[1];  // AI player

        _speed = player.paddle.speed;
        _background = GameObject.Find("Background");
        _screenBounds = _background.GetComponent<SpriteRenderer>().bounds.extents;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Observations are based on local positions.

        // Paddle position
        sensor.AddObservation(transform.localPosition);

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
}
