using UnityEngine;

public class Object2DPhysicsState : SimpleObjectChunk
{
    public SimpleValue<Vector2> Velocity = new SimpleValue<Vector2>(true, Vector2.zero);
}