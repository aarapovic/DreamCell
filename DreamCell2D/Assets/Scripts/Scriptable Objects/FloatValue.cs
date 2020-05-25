using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    public float runtimeValue;
    [HideInInspector]
    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue;
    }
    public void OnBeforeSerialize()
    {

    }
}
