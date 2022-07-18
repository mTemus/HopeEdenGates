using UnityEngine;

public class ObjectWorldPositionState : SimpleObjectChunk
{
    public SimpleValue<Vector3> Position = new SimpleValue<Vector3>(true);

    public override void InitializeAsNew(SimpleObject simpleObject)
    {
        base.InitializeAsNew(simpleObject);
        Position.Value = (simpleObject as SimpleObjectWithGameObject).CreatedGameObject.transform.position;
    }
}