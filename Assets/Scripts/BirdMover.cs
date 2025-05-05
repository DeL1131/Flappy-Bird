using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(BirdInput))]

public class BirdMover : MonoBehaviour
{
    [SerializeField] private float _tapForce;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxRotationZ;
    [SerializeField] private float _minRotationZ;

    private BirdInput _input;
    private Rigidbody2D _rigidbody2D;

    private Vector3 _startPosition;
    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private void Awake()
    {
        _input = GetComponent<BirdInput>();
    }

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _maxRotation = Quaternion.Euler(0, 0, _maxRotationZ);
        _minRotation = Quaternion.Euler(0, 0, _minRotationZ);
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotationSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        _input.SpacePressed += Jump;
    }

    private void OnDisable()
    {       
        _input.SpacePressed -= Jump;
    }

    public void Reset()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_speed, _tapForce);
        transform.rotation = _maxRotation;
    }
}