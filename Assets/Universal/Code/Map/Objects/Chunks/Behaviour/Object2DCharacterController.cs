using UnityEngine;

public class Object2DCharacterController : SimpleObjectChunk
{
    public Object2DCharacterController(SimpleObjectChunkPackageBase package) : base(package)
    {
        var concretePackage = package as Object2DCharacterControllerConstructorMono.Object2DCharacterControllerPackage;
        m_speed = concretePackage.Speed;
    }

    private Rigidbody2D m_rigidBody2D;
    private Object2DFacingDirectionState m_facingDirectionState;
    private ObjectWorldPositionState m_worldPositionState;
    private Object2DPhysicsState m_physicsState;
    private float m_speed;

    public override void PostInitialize()
    {
        base.PostInitialize();
        m_rigidBody2D = (SimpleObject as SimpleObjectWithGameObject).CreatedGameObject.GetComponent<Rigidbody2D>();
        m_facingDirectionState = SimpleObject.GetChunk<Object2DFacingDirectionState>();
        m_worldPositionState = SimpleObject.GetChunk<ObjectWorldPositionState>();
        m_physicsState = SimpleObject.GetChunk<Object2DPhysicsState>();
    }       

    public void Move(Vector2 direction)
    {
        m_rigidBody2D.velocity = direction * m_speed;
        m_worldPositionState.Position.Value = m_rigidBody2D.transform.position;
        m_physicsState.Velocity.Value = m_rigidBody2D.velocity;

        if (direction.x != 0)
            TurnCharacterFacingDirection(direction.x == 1f);
    }

    //TODO: facing is wrong when pressed A and W or D and W 
    private void TurnCharacterFacingDirection(bool isFacingRight)
    {
        var turnedRight = m_facingDirectionState.IsFacingRight.Value;

        if (isFacingRight == turnedRight)
            return;

        var objectTransform = m_rigidBody2D.transform;
        var objectScale = objectTransform.localScale;

        objectScale.x *= -1;
        objectTransform.localScale = objectScale;
        m_facingDirectionState.IsFacingRight.Value = isFacingRight;
    }
}