using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(BirdCollisionHandler))]
[RequireComponent(typeof(AttackCooldownHandler))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BirdInput))]
[RequireComponent (typeof(RangeAttacker))]

public class Bird : MonoBehaviour, IDamagable
{
    [SerializeField] private float _damage;

    private RangeAttacker _attacker;
    private BirdMover _birdMover;
    private ScoreCounter _scoreCounter;
    private BirdCollisionHandler _birdCollisionhandler;
    private Health _health;
    private BirdInput _input;
    private AttackCooldownHandler _attackCooldownHandler;

    public event Action<float> Damaged;
    public event Action GameOver;

    private void Awake()
    {
        _attacker = GetComponent<RangeAttacker>();
        _input = GetComponent<BirdInput>();
        _health = GetComponent<Health>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _birdCollisionhandler = GetComponent<BirdCollisionHandler>();
        _birdMover = GetComponent<BirdMover>();
        _attackCooldownHandler = GetComponent<AttackCooldownHandler>();
    }

    private void OnEnable()
    {
        _health.Die += EndGame;
        _input.Mouse0Pressed += Attack;
        _birdCollisionhandler.Collided += ProcessCollision;
    }

    private void OnDisable()
    {
        _health.Die -= EndGame;
        _input.Mouse0Pressed -= Attack;
        _birdCollisionhandler.Collided -= ProcessCollision;
    }
    public void Reset()
    {
        _scoreCounter.Reset();
        _birdMover.Reset();
        _health.Reset();
    }

    public void TakeDamage(float damage)
    {
        _health.DamageHealth(damage);
    }

    private void ProcessCollision(Collider2D collision)
    {
        if ((collision.TryGetComponent(out Ground ground)) || collision.TryGetComponent(out Wall wall))
        {
            GameOver?.Invoke();
        }
        if ((collision.TryGetComponent(out Enemy enemy)))
        {
            GameOver?.Invoke();
        }
    }

    private void EndGame()
    {
        GameOver?.Invoke();
    }

    private void Attack()
    {
        if (_attackCooldownHandler.CanAttack)
        {
            _attacker.ExecuteAttack();
            StartCoroutine(_attackCooldownHandler.CooldownRoutine());
        }
    }
}