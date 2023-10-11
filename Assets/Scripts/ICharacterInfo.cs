using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInfo
{
    public Vector3 GetPosition();
    public Vector3 GetVelocity();
    public float GetMaxSpeed();
    public Vector3 GetForward();
}
