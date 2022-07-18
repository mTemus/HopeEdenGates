using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//TODO: this probably will be a base for AI input as well (Flip/update movement) // but won't be added to ticker manager
public class Object2DMovementManualInput : SimpleObjectChunk
{
    public Object2DMovementManualInput(SimpleObjectChunkPackageBase package) : base(package)
    {
        var concretePackage = package as Object2DMovementManualInputConstructorMono.Object2DMovementManualInputPackage;

        m_movementAction = GameManager.Instance.InputManager.ActionMapSystem
            .GetActionMap(concretePackage.MovementActionMapType)
            .FindAction(concretePackage.MovementActionName);

        m_speed = concretePackage.MovementSpeed;
    }

    private readonly float m_speed;
    private bool m_facingRight;
    private readonly InputAction m_movementAction;

    private Object2DFacingDirectionState m_facingDirectionState;
    private ObjectWorldPositionState m_worldPositionState;

    public override void PostInitialize()
    {
        base.PostInitialize();
        m_worldPositionState = SimpleObject.GetChunk<ObjectWorldPositionState>();
        m_facingDirectionState = SimpleObject.GetChunk<Object2DFacingDirectionState>();
        m_facingRight = m_facingDirectionState.IsFacingRight.Value;
    }

    public override void OnIngredientEnabled()
    {
        base.OnIngredientEnabled();
        GameManager.Instance.InputManager.ActionMapSystem.CurrentActionMap.AddChangedListener(OnActionMapChange);
    }

    public override void OnIngredientDisabled()
    {
        base.OnIngredientDisabled();
        GameManager.Instance.InputManager.ActionMapSystem.CurrentActionMap.RemoveChangedListener(OnActionMapChange);
    }

    private void OnActionMapChange(SimpleValueBase value)
    {
        var actionMap = value.GetValueAs<InputActionMap>();

        if (actionMap.name == m_movementAction.actionMap.name)
            GameManager.Instance.TickerManager.AddObjectToTick(UpdateObjectPosition);
        else
            GameManager.Instance.TickerManager.RemoveObjectToTick(UpdateObjectPosition);

    }

    //TODO: movement is rusty, when pressed A and D character is stopping
    //TODO: facing is wrong when pressed A and W or D and W 
    private void UpdateObjectPosition(float timeDelta)
    {
        var movementDirection = m_movementAction.ReadValue<Vector2>();
        m_worldPositionState.Position.Value += new Vector3(movementDirection.x, movementDirection.y, 0f) * (m_speed * Time.deltaTime);

        if (movementDirection.x != 0)
            UpdateFacingDirection(movementDirection.x == -1f);
    }

    private void UpdateFacingDirection(bool turnedRight)
    {
        if (m_facingRight == turnedRight)
            return;

        m_facingRight = !m_facingRight;
        m_facingDirectionState.IsFacingRight.Value = m_facingRight;
    }
}