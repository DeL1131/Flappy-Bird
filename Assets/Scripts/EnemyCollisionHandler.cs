using UnityEngine;

public class EnemyCollisionHandler : CollisionHandler<Collider2D>
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        InvokeCollision(collision);
    }
}