using System;using UnityEngine;

public class Object2DCharacterControllerConstructorMono : MonoBehaviour, ISimpleObjectChunkConstructorMonoBase
{
    [Serializable]
    public class Object2DCharacterControllerPackage : SimpleObjectChunkPackageBase
    {
        public float Speed;
    }

    public Object2DCharacterControllerPackage Package;

    public SimpleObjectChunk Construct()
    {
        return new Object2DCharacterController(Package);
    }
}