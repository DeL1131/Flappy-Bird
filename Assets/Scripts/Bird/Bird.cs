using System;
using UnityEngine;

[RequireComponent(typeof(BirdMover))]
[RequireComponent(typeof(ScoreCounter))]
[RequireComponent(typeof(CollisionHandler))]
[RequireComponent(typeof(CooldownTimer))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(BirdInput))]
[RequireComponent (typeof(RangeAttacker))]

public class Bird : MonoBehaviour, IDamagable, IInteractable
{
    [SerializeField] private float _damage;
    [SerializeField] private float _attackCooldown;

    private RangeAttacker _attacker;
    private BirdMover _birdMover;
    private ScoreCounter _scoreCounter;
    private CollisionHandler _birdCollisionhandler;
    private Health _health;
    private BirdInput _input;
    private CooldownTimer _attackCooldownHandler;

    public event Action<float> Damaged;
    public event Action GameOver;

    private void Awake()
    {
        _attacker = GetComponent<RangeAttacker>();
        _input = GetComponent<BirdInput>();
        _health = GetComponent<Health>();
        _scoreCounter = GetComponent<ScoreCounter>();
        _birdCollisionhandler = GetComponent<CollisionHandler>();
        _birdMover = GetComponent<BirdMover>();
        _attackCooldownHandler = GetComponent<CooldownTimer>();
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
        if ((collision.TryGetComponent(out IInteractable interactable)))
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
        if (_attackCooldownHandler.IsReady)
        {
            _attacker.ExecuteAttack();
            _attackCooldownHandler.StartTimer(_attackCooldown);
        }
    }
}