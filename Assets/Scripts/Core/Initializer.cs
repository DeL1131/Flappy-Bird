using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] private GameObjectRegistry _registry;

    public GameObjectRegistry Registry => _registry;
}