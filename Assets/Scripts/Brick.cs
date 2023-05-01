using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public int hp = 1;  // Change later once bricks are added programatically
    public ParticleSystem DestructionEffect;

    private SpriteRenderer _sr;

    // Start is called before the first frame update
    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _sr.sprite = LevelManager.Instance.brickSprites[hp - 1];  // Delete later once bricks are added programatically
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        CollisionLogic(ball);
    }

    /// <summary>
    /// Handles collision logic.
    /// </summary>
    /// <param name="ball">The ball object that is colliding with the brick.</param>
    private void CollisionLogic(Ball ball)
    {
        hp--;
        if (hp <= 0)
        {
            ApplyDestructionEffect();
            Destroy(gameObject);
        }
        else
        {
            _sr.sprite = LevelManager.Instance.brickSprites[hp - 1];
        }
    }

    /// <summary>
    /// Applies a particle system effect based on the color of the destroyed brick.
    /// </summary>
    private void ApplyDestructionEffect()
    {
        GameObject effect = Instantiate(DestructionEffect.gameObject, transform.position, Quaternion.identity);
        ParticleSystem.MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = _sr.color;
    }
}
