using System.Collections.Generic;
using UnityEngine;

public class GameObjectRegistry : MonoBehaviour
{
    private List<GameObject> _gameObjects = new List<GameObject>();

    public void AddObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    public void RemoveObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }

    public void DeactivateTemporaryObjects()
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}