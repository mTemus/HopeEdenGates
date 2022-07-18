using UnityEngine;

public class Object2DCharacterController : SimpleObjectChunk
{
    private Rigidbody2D m_rigidBody2D;

    public override void PostInitialize()
    {
        base.PostInitialize();
        m_rigidBody2D = (SimpleObject as SimpleObjectWithGameObject).CreatedGameObject.GetComponent<Rigidbody2D>();
    }

    public override void OnIngredientEnabled()
    {
        base.OnIngredientEnabled();
        SimpleObject.GetChunk<ObjectWorldPositionState>().Position.AddChangedListener(OnWorldPositionChanged);
        SimpleObject.GetChunk<Object2DFacingDirectionState>().IsFacingRight.AddChangedListener(TurnCharacterFacingDirection);
    }

    public override void OnIngredientDisabled()
    {
        base.OnIngredientDisabled();
        SimpleObject.GetChunk<ObjectWorldPositionState>().Position.RemoveChangedListener(OnWorldPositionChanged);
        SimpleObject.GetChunk<Object2DFacingDirectionState>().IsFacingRight.RemoveChangedListener(TurnCharacterFacingDirection);
    }

    private void OnWorldPositionChanged(SimpleValueBase value)
    {
        var newPos = value.GetValueAs<Vector3>();
        m_rigidBody2D.transform.position = newPos;
    }

    private void TurnCharacterFacingDirection(SimpleValueBase value)
    {
        var isFacingRight = value.GetValueAs<bool>();

        var objectTransform = m_rigidBody2D.transform;

        if (isFacingRight && objectTransform.position.x == 1f)
            return;

        var objectScale = objectTransform.localScale;

        objectScale.x *= -1;
        objectTransform.localScale = objectScale;
    }
}