using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectRegistryReference", menuName = "Registry/GameObjectRegistryReference", order = 51)]

public class GameObjectRegistryReference : ScriptableObject
{
    private GameObjectRegistry _registry;

    public GameObjectRegistry Registry => _registry;

    public void SetRegistry(GameObjectRegistry registry)
    {
        _registry = registry;
    }
}