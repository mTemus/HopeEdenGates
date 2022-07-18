using System;
using UnityEngine;

public class Object2DMovementManualInputConstructorMono : MonoBehaviour, SimpleObjectChunkConstructorMonoBase
{
    [Serializable]
    public class Object2DMovementManualInputPackage : SimpleObjectChunkPackageBase
    {
        public ActionMapSystem.ActionMapType MovementActionMapType;
        public string MovementActionName;
        
        public float MovementSpeed;
    }

    public Object2DMovementManualInputPackage Package;

    public SimpleObjectChunk Construct()
    {
        return new Object2DMovementManualInput(Package);
    }
}
