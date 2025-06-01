using UnityEngine;

[RequireComponent(typeof(Collider2D))]

public class BirdCollisionHandler : CollisionHandler<Collider2D>
{
    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        InvokeCollision(other);
    }
}