using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Events
{
    //T0
    public delegate void StringEvent(string val);

    public delegate void Vector2Event(Vector2 val);

    public delegate void Vector3Event(Vector3 val);

    public delegate void IntEvent(int val);

    public delegate void FloatEvent(float val);

    public delegate void ObjectEvent(object val);

    public delegate void GameObjectEvent(GameObject val);

    public delegate void Event();

    public delegate void BoolEvent(bool val);

    //Compare Events T0,T1
    [System.Serializable] public delegate void GameObjectCompareEvent(GameObject a, GameObject b);

    [System.Serializable] public delegate void Vector3CompareEvent(Vector3 a, Vector3 b);
}