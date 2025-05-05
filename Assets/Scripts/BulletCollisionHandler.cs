using UnityEngine;

public class BulletCollisionHandler : CollisionHandler<Collider2D>
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeCollision(collision);
    }
}