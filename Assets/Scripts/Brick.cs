using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public static event Action<Brick> OnBrickDestruction;

    public Player player;

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
    /// <param name="owner">The player associated with the brick.</param>
    /// <param name="containerTransform">The transform component of the parent container.</param>
    /// <param name="sprite">The brick's sprite image.</param>
    /// <param name="hitpoints">The brick's hitpoints.</param>
    public void Init(Player owner, Transform containerTransform, Sprite sprite, Color color, int hitpoints)
    {
        player = owner;
        transform.SetParent(containerTransform);
        _sr.sprite = sprite;
        _sr.color = color;
        hp = initialHp = hitpoints;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hp--;
        if (hp <= 0)
        {
            player.RemainingBricks--;
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
