using UnityEngine;

// Firewall pattern: https://www.youtube.com/watch?v=ZwLekxsSY3Y
public class Object2DAnimatorController : SimpleObjectChunk
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Move = Animator.StringToHash("Run");

    private Animator m_animator;
    private int m_currentState;

    public override void InitializeAsNew(SimpleObject simpleObject)
    {
        base.InitializeAsNew(simpleObject);
        m_animator = (simpleObject as SimpleObjectWithGameObject).CreatedGameObject.GetComponent<Animator>();
        m_currentState = Idle;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SimpleObject.GetChunk<Object2DPhysicsState>().Velocity.AddChangedListener(OnVelocityChanged);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SimpleObject.GetChunk<Object2DPhysicsState>().Velocity.RemoveChangedListener(OnVelocityChanged);
    }

    private void OnVelocityChanged(SimpleValueBase value)
    {
        var direction = value.GetValueAs<Vector2>();
        var newState = direction.x != 0 || direction.y != 0 ? Move : Idle;

        if (newState == m_currentState)
            return;

        m_animator.CrossFade(newState, 0, 0);
        m_currentState = newState;
    }
}