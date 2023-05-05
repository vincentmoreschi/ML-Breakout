using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public static event Action<Brick> OnBrickDestruction;

    public int hp = 1;
    public int initialHp { get; private set; }
    public ParticleSystem DestructionEffect;

    private SpriteRenderer _sr;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Initialize the brick.
    /// </summary>
    /// <param name="containerTransform">The transform component of the parent container.</param>
    /// <param name="sprite">The brick's sprite image.</param>
    /// <param name="hitpoints">The brick's hitpoints.</param>
    internal void Init(Transform containerTransform, Sprite sprite, int hitpoints)
    {
        transform.SetParent(containerTransform);
        _sr.sprite = sprite;
        hp = initialHp = hitpoints;
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
            LevelManager.Instance.RemainingBricks--;
            OnBrickDestruction?.Invoke(this);
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
