using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour {
    public void SetPositionTo(Transform t)
    {
        transform.position = t.position;
    }
}
