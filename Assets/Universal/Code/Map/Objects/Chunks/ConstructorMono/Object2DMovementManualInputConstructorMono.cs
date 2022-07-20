using System;
using UnityEngine;

public class Object2DMovementManualInputConstructorMono : MonoBehaviour, ISimpleObjectChunkConstructorMonoBase
{
    [Serializable]
    public class Object2DMovementManualInputPackage : SimpleObjectChunkPackageBase
    {
        public ActionMapSystem.ActionMapType MovementActionMapType;
        public string MovementActionName;
    }

    public Object2DMovementManualInputPackage Package;

    public SimpleObjectChunk Construct()
    {
        return new Object2DMovementManualInput(Package);
    }
}
