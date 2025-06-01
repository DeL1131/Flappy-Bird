using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime);
    }
}