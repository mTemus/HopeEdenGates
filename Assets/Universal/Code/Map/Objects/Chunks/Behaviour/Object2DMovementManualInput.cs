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

    }

    private readonly InputAction m_movementAction;

    private Object2DCharacterController m_object2DCharacterController;

    public override void PostInitialize()
    {
        base.PostInitialize();
        m_object2DCharacterController = SimpleObject.GetChunk<Object2DCharacterController>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GameManager.Instance.InputManager.ActionMapSystem.CurrentActionMap.AddChangedListener(OnActionMapChange);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        GameManager.Instance.InputManager.ActionMapSystem.CurrentActionMap.RemoveChangedListener(OnActionMapChange);
    }

    private void OnActionMapChange(SimpleValueBase value)
    {
        var actionMap = value.GetValueAs<InputActionMap>();

        if (actionMap.name == m_movementAction.actionMap.name)
            GameManager.Instance.TickerManager.AddObjectToTick(UpdateObjectPosition, TickerManager.TickType.FixedUpdate);
        else
            GameManager.Instance.TickerManager.RemoveObjectToTick(UpdateObjectPosition);

    }

    //TODO: movement is rusty, when pressed A and D character is stopping
    private void UpdateObjectPosition(float timeDelta)
    {
        var movementDirection = m_movementAction.ReadValue<Vector2>();
        m_object2DCharacterController.Move(movementDirection);
    }
}